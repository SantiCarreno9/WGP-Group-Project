namespace Puzzle
{
    public enum PuzzleType
    {
        Door_Key
    }
    public interface IPuzzle
    {
        PuzzleType Type { get; }
        void SetUp();
        void Solve();
    }
}