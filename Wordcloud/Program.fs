open System.Drawing

[<EntryPoint>]
let main argv = 
    use img = new Bitmap(1024, 1024)
    use g = Graphics.FromImage(img)
    g.Clear(Color.White)

    System.IO.File.ReadAllText("test.txt")
    |> Words.getWords
    |> fun l -> Map.empty |> Words.countWords l
    |> Map.toList
    |> List.sortWith (fun (_, v1) (_, v2) -> v2 - v1)
    |> fun lst ->
        let (_, max) = List.head lst
        List.map (fun (k, v) -> (k, float v / float max)) lst
    |> CloudCreator.drawCloud g [] (512.0f, 512.0f) 0.0
    
    img.Save("test.png")

    ignore <| System.Console.ReadKey()
    0 // return an integer exit code
