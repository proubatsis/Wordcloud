module Words
    let filterNewLine (str:string) =
        str.Replace("\r", "").Replace("\n", " ")

    let filterSpecial (str:string) =
        let isSpecial (c:char) =
            let ac = c |> int
            if ac >= 33 && ac <= 47 || ac >= 58 && ac <= 64 || ac >= 91 && ac <= 96 || ac >= 123 && ac <= 126 then true else false

        let rec filter (lst:char list) =
            match lst with
            | [] -> ""
            | a::t when isSpecial a -> filter t
            | a::t -> a.ToString() + filter t

        str.ToCharArray()
        |> List.ofArray
        |> filter

    let filterCommon =
        let commonWords = ["the"; "will"; "that"; "with"; "when"; "able"; "must"; "come"; "which"; "some"; "they"; "into"; "still";
        "their"; "there"; "from"; "this"; "those"; "cannot"; "have"; "back"; "here"; "what"; "hath"; "than"; "then"; "know"; "your"; "also"; "upon"]

        List.filter (fun (s:string) -> not <| List.exists (fun cs -> s = cs || s.Length <= 3) commonWords)

    let getWords (str:string) =
        str.ToLower()
        |> filterNewLine
        |> filterSpecial
        |> fun s ->
            s.Split(' ')
            |> List.ofArray
            |> filterCommon

    let rec countWords words counts =
        match words with
        | [] -> counts
        | a::t ->
            Map.fold (fun s k v -> if k = a then v else s) 0 counts
            |> fun value -> Map.add a (value + 1) counts
            |> countWords t

