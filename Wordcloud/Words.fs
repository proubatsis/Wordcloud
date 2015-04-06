module Words
    let filterNewLine (str:string) =
        str.Replace("\r", "").Replace("\n", " ")

    let filterSpecial (str:string) =
        let isSpecial (c:char) =
            let ac = System.Convert.ToByte(c) |> int
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
        let commonWords = [""; "the"; "if"; "a"; "to"; "is"; "i"; "will"; "be"; "we"; "in"; "at"; "of";
        "and"; "are"; "all"; "that"; "with"; "his"; "when"; "able"; "go"; "as"; "let"; "for"; "by"; "but";
        "you"; "must"; "come"; "our"; "which"; "not"; "on"; "an"; "out"; "no"; "has"; "it"; "am"; "us"; "to";
        "too"; "who"; "up"; "so"; "some"; "they"; "into"; "still"; "their"; "there"; "can"; "from"; "this"]

        List.filter (fun s -> not <| List.exists (fun cs -> s = cs) commonWords)

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

