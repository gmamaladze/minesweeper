module Others

type Point = {X:int; Y:int}

type Scale = Point

type Size = {Width:int; Height:int} with 
    static member (+) (left:Size, right:Point) : Size = 
        { Width = left.Width + right.X ; Height = left.Height + right.Y }
    
    static member (*) (left:Size, right:Scale) =
        { Width = left.Width*right.X ; Height=left.Height*right.Y }

    member x.FitsInto(other:Size) =
        (x.Width <= other.Width) && (x.Height <= other.Height)

    member x.AllPoints =
        seq {
            for i in 0..x.Width do
                for j in 0..x.Height do
                        yield { X=i; Y=j}
        }

type GemResult = {HasFailed:bool; CoveredCount:int} with
    member x.IsGame = ( x.HasFailed || x.CoveredCount = 0)

type Covers = {Covers: Map<Point, bool>} with
    member x.SwitchFlag (point: Point) =
        match x.Covers.TryFind(point) with
            | Some hasFlag -> {Covers = x.Covers.Remove(point).Add(point, not hasFlag)}
            | None -> x

    member x.Uncover (point: Point) =
        match x.IsCovered(point) with
            | true -> {Covers = x.Covers.Remove(point)}
            | _ -> x

    member x.UncoverRange points = {Covers = x.Covers.Remove(points)} //TODO: Remove mutiple elements

    member x.IsCovered ( point : Point ) = x.Covers.ContainsKey(point);

    member x.HasFlag ( point:Point ) = x.Covers.Item(point)

    member x.CoverCount = x.Covers.Count

    static member Create (size : Size) =
        {Covers = size.AllPoints |> Seq.map(fun p -> p, false) |> Map }
