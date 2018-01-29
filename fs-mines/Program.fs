module Program

open Adapter
open Board
open Fmines.Ui
open Gameplay
open Geometry
open System

[<EntryPoint>]
let main argv = 
    let options = OptionSelector.ReadOptions()
    let mineField = MineFiedlBuilder.GenerateRandom(options.MinesCount, c options.Size)
    let gameState = Game.Start(mineField)
    using (Graphics.Init(options.Size)) (fun graphics -> 
        Title.Draw(graphics, "SPACE-falg  ENTER-open  Q-quit  U-undo")
        Grid.Draw(graphics)
        let rec loop (graphics : Graphics, mineField : MineField, gameState : GameState) = 
            Draw(graphics, mineField, gameState)
            let key = Console.ReadKey(true).Key
            let gameState = Game.ExecuteCommand(key, gameState, mineField)
            let result = gameState.Evaluate(mineField)
            match result.IsGameOver with 
            | true -> (result, gameState)
            | false -> loop (graphics, mineField, gameState)
        
        let result, finalState = loop (graphics, mineField, gameState)
        let covers = gameState.Covers.UncoverRange(seq (mineField.Mines) |> List.ofSeq)
        Draw(graphics, mineField, Game.Quit(finalState, mineField))
        Title.Draw(graphics, 
                   match result.HasFailed with 
                   | true -> "Sorry, you lost this game. Better luck next time!"
                   | false -> "Congratulations, you won the game! Press any key to EXIT ...")
        Console.ReadKey true |> ignore)
    0
