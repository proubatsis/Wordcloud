open System.Windows.Forms
open System.Drawing

[<EntryPoint>]
let main argv = 
    //Get the information to use for the word cloud
    System.Console.Write("Input File:  ")
    let textFile = System.Console.ReadLine()
    System.Console.Write("Output File: ")
    let imageFile = System.Console.ReadLine()

    if not <| System.IO.File.Exists textFile then
        System.Console.WriteLine("The text file - {0} - does not exist!", textFile)
        System.Console.ReadKey() |> ignore
        0
    else
        use img = new Bitmap(1024, 1024)
        use g = Graphics.FromImage(img)
        g.Clear(ColorSchemes.BG_RGB)

        System.IO.File.ReadAllText(textFile)
        |> Words.getWords
        |> fun l -> Map.empty |> Words.countWords l
        |> Map.toList
        |> List.sortWith (fun (_, v1) (_, v2) -> v2 - v1)
        |> fun lst ->
            let (_, max) = List.head lst
            List.map (fun (k, v) -> (k, float v / float max)) lst
        |> CloudCreator.drawCloud g [] (512.0f, 512.0f) 0.0 ColorSchemes.RGB

        img.Save(imageFile)
    
        //Create the form and display the image
        use cloudPanel = new Panel()
        cloudPanel.Paint.Add(fun e -> e.Graphics.DrawImage(img, new Point(0, 0)))
        cloudPanel.Size <- new Size(1024, 1024)

        use form = new Form()
        form.Text <- "Word Cloud"
        form.Size <- new Size(1024, 1024)
        form.Controls.Add cloudPanel
        Application.Run(form)

        0 // return an integer exit code
