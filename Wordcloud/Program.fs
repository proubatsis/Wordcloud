(*
    Created by Panagiotis Roubatsis

    Description: An application that creates
    a word cloud given a text file. It displays
    the text file in a form and then saves the image
    to a file.
*)

open System.Windows.Forms
open System.Drawing

[<EntryPoint>]
let main argv = 
    //Get the information to use for the word cloud
    System.Console.Write("Input File:  ")
    let textFile = System.Console.ReadLine()
    System.Console.Write("Output File: ")
    let imageFile = System.Console.ReadLine()
    System.Console.WriteLine("\nEnter the letter of your desired color scheme.\na)RGB b)Bright c)Dark")
    let colorChoice = System.Console.ReadKey().KeyChar

    let (background, colors) =
        match colorChoice with
        | 'b' -> (ColorSchemes.BG_Bright, ColorSchemes.Bright)
        | 'c' -> (ColorSchemes.BG_Dark, ColorSchemes.Dark)
        | _ -> (ColorSchemes.BG_RGB, ColorSchemes.RGB)

    //Make sure the file exists before continuing
    if not <| System.IO.File.Exists textFile then
        System.Console.WriteLine("The text file - {0} - does not exist!", textFile)
        System.Console.ReadKey() |> ignore
        0
    else
        //Create a blank image
        let (width, height) = (1024, 1024)
        use img = new Bitmap(width, height)
        use g = Graphics.FromImage(img)
        g.Clear(background)

        (*  Read the file, count the words,
            sort the count in descending order,
            normalize the counts relative to the largest count,
            draw the word cloud to the image.
        *)
        System.IO.File.ReadAllText(textFile)
        |> Words.getWords
        |> fun l -> Map.empty |> Words.countWords l
        |> Map.toList
        |> List.sortWith (fun (_, v1) (_, v2) -> v2 - v1)
        |> fun lst ->
            let (_, max) = List.head lst
            List.map (fun (k, v) -> (k, float v / float max)) lst
        |> CloudCreator.drawCloud g [] (float32 width / 2.0f, float32 height / 2.0f) 0.0 colors

        //Save the image to a file
        img.Save(imageFile)
    
        //Create the form and display the image
        use cloudPictureBox = new PictureBox()
        cloudPictureBox.Image <- img
        cloudPictureBox.Size <- new Size(width, height)

        use form = new Form()
        form.Text <- "Word Cloud"
        form.Size <- new Size(width, width)
        form.Controls.Add cloudPictureBox
        Application.Run(form)

        0 // return an integer exit code
