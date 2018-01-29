open System
open Fmines.Ui
//open Fmines

open Geometry
open Board
open Gameplay
open Adapter


[<EntryPoint>]
let main argv = 
    //printfn "%A" argv
    let options = OptionSelector.ReadOptions()
    let mineField = MineFiedlBuilder.GenerateRandom(options.MinesCount, c options.Size )
    let gameState = Game.Start(mineField)
    using (Graphics.Init(options.Size)) (fun graphics ->
        Title.Draw(graphics, "SPACE-falg  ENTER-open  Q-quit  U-undo")
        Grid.Draw(graphics)
        let rec loop(graphics: Graphics, mineField: MineField, gameState: GameState) =
            Draw(graphics, mineField, gameState)
            let key = Console.ReadKey(true).Key
            let gameState = Game.ExecuteCommand(key, gameState, mineField)
            let result = gameState.Evaluate(mineField)
            if result.IsGameOver then result 
            else loop(graphics, mineField, gameState)
        let result = loop(graphics, mineField, gameState)
        //let allMinesUncovered = gameState.Covers.UncoverRange(seq(mineField.Mines)|>List.ofSeq);
        //Board.Draw(
        //    graphics
        //    ,mineField
        //    ,allMinesUncovered);
        Title.Draw(
            graphics,
            if result.HasFailed then "Sorry, you lost this game. Better luck next time!" 
            else "Congratulations, you won the game!"
            , "Press any key to EXIT ...")
    )
    Console.ReadKey true |> ignore 
    0