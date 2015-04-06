(*
    Created by Panagiotis Roubatsis
    Description: This module is used to create
    a GUI using windows forms.
*)

module GUI
    open System.Windows.Forms
    open System.Drawing

    let createForm title (width, height) =
        let form = new Form()
        form.Text <- title
        form.Size <- new Size(width, height)
        form

    let addControlToForm control (form:Form) =
        form.Controls.Add control
        form

    let createPictureBox (width, height) (x, y) image =
        let pictureBox = new PictureBox()
        pictureBox.Location <- new Point(x, y)
        pictureBox.Size <- new Size(width, height)
        pictureBox.Image <- image
        pictureBox

