namespace v1
{
    class Chain
    {
        public int LengthOfChain { get; set; }
        public int WholeLenghtOfChain { get; set; } //"sadsad" + "    "  
        public int CurrentLocationHead { get; set; }

        public Chain()
        {
            LengthOfChain = HelperConst.rand.Next(HelperConst.MINHEIGHTOFCHAIN, HelperConst.MAXHEIGHTOFCHAIN);
            WholeLenghtOfChain = LengthOfChain + HelperConst.rand.Next(HelperConst.MINSPACE, HelperConst.MAXSPACE);
            CurrentLocationHead = 0;
        }
    }
}
