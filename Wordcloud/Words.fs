(*
    Created by Panagiotis Roubatsis

    Description: Used to parse a chunk of text to find
    and count the words.
*)

module Words
    let filterNewLine (str:string) =
        str.Replace("\r", "").Replace("\n", " ")

    //Remove special characters from a string
    let filterSpecial (str:string) =
        let isSpecial (c:char) =
            let ac = c |> int
            ac >= 33 && ac <= 47 || ac >= 58 && ac <= 64 || ac >= 91 && ac <= 96 || ac >= 123 && ac <= 126

        let rec filter (lst:char list) =
            match lst with
            | [] -> ""
            | a::t when isSpecial a -> filter t
            | a::t -> a.ToString() + filter t

        str.ToCharArray()
        |> List.ofArray
        |> filter

    //Remove words that are <= 3 characters long and remove common words
    let filterCommon =
        let commonWords = ["the"; "will"; "that"; "with"; "when"; "able"; "must"; "come"; "which"; "some"; "they"; "into"; "still";
        "their"; "there"; "from"; "this"; "those"; "cannot"; "have"; "back"; "here"; "what"; "hath"; "than"; "then"; "know"; "your"; "also"; "upon"]

        List.filter (fun (s:string) -> not <| List.exists (fun cs -> s = cs || s.Length <= 3) commonWords)

    (*  Given a string it returns all the words in the string
        with common words filtered out. *)
    let getWords (str:string) =
        str.ToLower()
        |> filterNewLine
        |> filterSpecial
        |> fun s ->
            s.Split(' ')
            |> List.ofArray
            |> filterCommon

    (*  Count every occurence of a word in a list of words.
        return a map with a string as the key and an int as the value.    *)
    let rec countWords words counts =
        match words with
        | [] -> counts
        | a::t ->
            Map.fold (fun s k v -> if k = a then v else s) 0 counts
            |> fun value -> Map.add a (value + 1) counts
            |> countWords t

