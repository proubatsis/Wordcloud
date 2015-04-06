﻿module CloudCreator
    let ANGLE_INCREMENT = System.Math.PI / 16.0

    open System.Drawing
    let rec findCoord (boxes:RectangleF list) size (cx, cy) (dx, dy) k =
        let (x, y) = (dx * k + float cx, dy * k + float cy)
        let box = new RectangleF(new PointF(float32 x, float32 y), size)
        if List.exists (fun r -> box.IntersectsWith r) boxes then findCoord boxes size (cx, cy) (dx, dy) (k + 1.0) else (x, y)

    let rec drawCloud (g:Graphics) (boxes:RectangleF list) (cx, cy) angle = function
        | [] -> ()
        | (k, v)::t ->
            let font = new Font(FontFamily.GenericSansSerif, v * 4 |> float32)
            let size = g.MeasureString(k, font)
            match boxes with
            | [] ->
                let box = new RectangleF(cx - size.Width / 2.0f, cy - size.Height / 2.0f, size.Width, size.Height)
                g.DrawString(k, font, Brushes.Green, box.X, box.Y)
                drawCloud g (List.append [box] boxes) (cx, cy) (angle + ANGLE_INCREMENT) t
            | _ ->
                let (dx, dy) = (cos angle, sin angle)
                let (x, y) = findCoord boxes size (cx, cy) (dx, dy) 10.0
                let box = new RectangleF(float32 x, float32 y, size.Width, size.Height)
                g.DrawString(k, font, Brushes.Green, box.X, box.Y)
                drawCloud g (List.append [box] boxes) (cx, cy) (angle + ANGLE_INCREMENT) t