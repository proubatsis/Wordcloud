module ColorSchemes
    open System.Drawing

    let RGB = seq { while true do yield! [Color.Red; Color.Green; Color.Blue]}
    let Bright = seq {while true do yield! [Color.Lime; Color.Pink; Color.Yellow; Color.Orange]}
    let Dark = seq {while true do yield! [Color.Navy; Color.Purple; Color.DarkRed; Color.DarkGreen]}

    let BG_RGB = Color.White
    let BG_Bright = Color.DarkGray
    let BG_Dark = Color.LightGray
