namespace Models
{
    public class Slab
    {
        public double FinalRate { get; set; }
        public SlabBracket[] Brackets { get; set; }

        public Slab(SlabBracket[] brackets)
        {
            Brackets = brackets;
        }
    }
}
