open System
open SixLabors.ImageSharp
open SixLabors.ImageSharp.Processing
open System.IO
open System.Diagnostics


let showImage path =
        let imgProcessInfo = ProcessStartInfo()
        imgProcessInfo.UseShellExecute <- true
        imgProcessInfo.FileName <- path
        Diagnostics.Process.Start(imgProcessInfo) |> ignore

let prefixFileName (path: string) (prefix: string) =
        sprintf "%s-%s" prefix (Path.GetFileName(path))

[<EntryPoint>]
let main argv =
        match argv with
        | [|imagePath|] ->
                use img = Image.Load(imagePath)
                printfn "Using image file: %s, width %d, height %d" imagePath img.Width img.Height
                use blurredImg = img.Clone(fun i -> i.GaussianBlur(5.0f) |> ignore)
                let tmpBlurPath = Path.Combine(Path.GetTempPath(), prefixFileName imagePath "blurred")
                printfn "%s" tmpBlurPath
                blurredImg.Save(tmpBlurPath)
                showImage imagePath
                showImage tmpBlurPath
                use grayscaleImg = img.Clone(fun i -> i.Grayscale() |> ignore)
                let grayscaleImgPath = Path.Combine(Path.GetDirectoryName(imagePath), prefixFileName  imagePath "gray")
                grayscaleImg.Save(grayscaleImgPath)
        | _ -> failwith "Must have only one argument."
        0

