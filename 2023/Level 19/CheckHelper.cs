record CheckHelper(long MoreThan, long LessThan)
{
    public CheckHelper CheckMoreThan(long moreThan)
    {
        return new CheckHelper(Math.Max(MoreThan, moreThan), LessThan);
    }

    public CheckHelper CheckLessThan(long lessThan)
    {
        return new CheckHelper(MoreThan, Math.Min(LessThan, lessThan));
    }

    public long ValidNumbers()
    {
        if (MoreThan > LessThan)
        {
            return 0;
        }

        return LessThan - MoreThan - 1;
    }
}