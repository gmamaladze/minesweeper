module Board

open Geometry
open System

let rec removeAll (map : Map<_, _>, keys : list<_>) = 
    match keys with
    | [] -> map
    | key :: rest -> removeAll (map.Remove(key), rest)

type Covers = 
    { Covers : Map<Point, bool> }
    
    static member Create(size : Size) = 
        { Covers = 
              size.AllPoints
              |> Seq.map (fun p -> p, false)
              |> Map }
    
    member x.SwitchFlag(point : Point) = 
        match x.Covers.TryFind(point) with
        | Some hasFlag -> { Covers = x.Covers.Remove(point).Add(point, not hasFlag) }
        | None -> x
    
    member x.Uncover(point : Point) = 
        match x.IsCovered(point) with
        | true -> { Covers = x.Covers.Remove(point) }
        | _ -> x
    
    member x.UncoverRange(points : list<Point>) = { Covers = removeAll (x.Covers, points) }
    member x.IsCovered(point : Point) = x.Covers.ContainsKey(point)
    member x.HasFlag(point : Point) = x.Covers.Item(point)
    member x.CoverCount = x.Covers.Count

type MineField = 
    { Size : Size
      Mines : Set<Point>
      Warnings : Map<Point, int> }
    member x.HasMineAt(point : Point) = x.Mines.Contains(point)
    
    member x.WarningsAt(point : Point) = 
        match x.Warnings.TryFind(point) with
        | Some result -> result
        | None -> 0
    
    member x.HasWarningAt(point : Point) = x.Warnings.ContainsKey(point)
    member x.IsEmptyAt(point : Point) = not (x.HasMineAt(point)) && not (x.HasWarningAt(point))

module MineFiedlBuilder = 
    let private CreateRandomMines(minesCount : int, size : Size) = 
        let rnd = System.Random()
        Seq.initInfinite (fun _ -> 
            { X = rnd.Next(size.Width)
              Y = rnd.Next(size.Height) })
        |> Seq.take (minesCount)
        |> Set
    
    let private CalculateWarnings(mines : seq<Point>, size : Size) = 
        mines
        |> Seq.map (fun m -> m.Neighbours(size))
        |> Seq.concat
        |> Seq.groupBy (fun p -> p)
        |> Seq.map (fun (k, g) -> (k, g |> Seq.length))
        |> Map.ofSeq
    
    let public GenerateRandom(minesCount : int, size : Size) = 
        let mines = CreateRandomMines(minesCount, size)
        let warnings = CalculateWarnings(mines, size)
        { Size = size
          Mines = mines
          Warnings = warnings }

type Covers with
    member x.UncoverDeep(mineField : MineField, point : Point) = 
        if (not (x.IsCovered(point))) then x
        else if (not (mineField.IsEmptyAt(point))) then x.Uncover(point)
        else 
            let seed = x.Uncover(point)
            point.Neighbours(mineField.Size) |> Seq.fold (fun acc elem -> acc.UncoverDeep(mineField, elem)) seed
