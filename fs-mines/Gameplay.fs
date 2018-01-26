module Gameplay

open System
open Geometry
open Board

type GemResult = {HasFailed:bool; CoveredCount:int} with
    member x.IsGame = ( x.HasFailed || x.CoveredCount = 0)

type GameState = {Moves : List<Covers> ; CursorPosition : Point} with
    member x.Do ( covers: Covers ) =
        {Moves = covers :: x.Moves ;  CursorPosition = x.CursorPosition}

    member x.Do ( cursorPosition: Point ) =
        {Moves = x.Moves; CursorPosition = cursorPosition}

    member x.Undo =
        match x.Moves with
            | [] -> x
            | _ -> { Moves=x.Moves.Tail ; CursorPosition=x.CursorPosition }

    member x.Covers = 
        x.Moves |> Seq.head


    member x.Evaluate (mineField:MineField) =
        { 
            HasFailed = mineField.Mines |> Seq.where(fun mine -> x.Covers.IsCovered(mine)) |> Seq.isEmpty |> not
            CoveredCount = x.Covers.CoverCount - mineField.Mines.Count 
        }
    
    static member Create(covers: Covers) =  
        {Moves=[covers]; CursorPosition={X=0; Y=0}}

                
module Game =
    let Start( mineField : MineField ) = 
        GameState.Create(Covers.Create(mineField.Size));

    let Undo( current: GameState) =
        current.Undo

    let Quit(current: GameState, mineField: MineField) =
        let range = seq(mineField.Mines) |> List.ofSeq
        let move = current.Covers.UncoverRange(range) 
        current.Do move

    let Uncover(current: GameState, mineField: MineField) =
        let move = current.Covers.UncoverDeep(mineField, current.CursorPosition)
        current.Do move

    let SwitchFlag(current: GameState) = 
        let move = current.Covers.SwitchFlag(current.CursorPosition)
        current.Do move

    let MoveCursor(current: GameState, mineField: MineField , direction : Direction ) =
        let move = current.CursorPosition.Move(direction, mineField.Size)
        current.Do move
