open System
open Fmines.Ui
open Fmines

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    let options = OptionSelector.ReadOptions()
    let mineField = MineFiedlBuilder.GenerateRandom(options.MinesCount, options.Size)
    let gameState = Game.Start(mineField)
    using (Graphics.Init(options.Size)) (fun graphics ->
        let rec loop(graphics: Graphics, mineField: MineField, gameState: GameState) : GameResult =
            Board.Draw(graphics, mineField, gameState.Covers());
            Cursor.Draw(graphics, gameState.CursorPosition);
            let key = Console.ReadKey(true).Key
            let command = key.ToCommand()
            let gameState = command.Invoke(gameState, mineField)
            let result = gameState.Evaluate(mineField)
            if result.IsGameOver() then result 
            else loop(graphics, mineField, gameState)
        let result = loop(graphics, mineField, gameState)
        if result.HasFailed then 1
        else 0
    )