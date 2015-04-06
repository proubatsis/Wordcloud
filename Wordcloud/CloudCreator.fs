module CloudCreator
    open System.Drawing
    let rec findCoord (boxes:RectangleF list) size (cx, cy) angle k =
        let (dx, dy) = (cos angle, sin angle)
        let (x, y) = (dx * k + float cx, dy * k + float cy)
        let box = new RectangleF(new PointF(float32 x, float32 y), size)
        if List.exists (fun r -> box.IntersectsWith r) boxes then findCoord boxes size (cx, cy) (angle + System.Math.PI / 180.0) (k + 1.0) else (x, y)

    let rec drawCloud (g:Graphics) (boxes:RectangleF list) (cx, cy) angle colors = function
        | [] -> ()
        | (k, v)::t ->
            let fontSize = 2.0 ** (3.0 * (v - 1.0))
            let font = new Font(FontFamily.GenericSansSerif, fontSize * 64.0 |> float32)
            let size = g.MeasureString(k, font)
            match boxes with
            | [] ->
                let box = new RectangleF(cx - size.Width / 2.0f, cy - size.Height / 2.0f, size.Width, size.Height)
                g.DrawString(k, font, new SolidBrush(Seq.pick (fun c -> Some(c)) colors), box.X, box.Y)
                drawCloud g [box] (cx, cy) angle (Seq.skip 1 colors) t
            | _ ->
                let (x, y) = findCoord boxes size (cx, cy) angle 10.0
                let box = new RectangleF(float32 x, float32 y, size.Width, size.Height)
                g.DrawString(k, font, new SolidBrush(Seq.pick (fun c -> Some(c)) colors), box.X, box.Y)

                let k = if v > 0.5 then 4.0 else 32.0
                drawCloud g (List.append [box] boxes) (cx, cy) (angle + System.Math.PI / k) (Seq.skip 1 colors) t
