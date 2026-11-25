namespace Events;

public class Auction
{
    public string Item { get; set; }
    public decimal StartPrice { get; set; }
    public decimal Step{ get; set; }
    public string? CurrentLeader { get; private set; } 
    public decimal CurrentPrice { get; private set; }
    private bool isClosed = false;
    
    public Auction(string item, decimal startPrice, decimal step)
    {
        Item = item;
        StartPrice = startPrice;
        Step = step;
    }

    public event BidHandler? BidPlaced;
    public event OutbidHandler? Outbid;
    public event EndedHandler? Ended;

    public bool PlaceBid(string bidder, decimal amount)
    {
        if (isClosed)
            return false;
        
        if(CurrentLeader == bidder)
            return false;
        
        if (CurrentLeader == null)
        {
            if (amount < StartPrice)
                return false;
        }
        else
        {
            if (amount < CurrentPrice + Step)
                return false;
        }
        string? PreviousLeader = CurrentLeader;
        decimal PreviousPrice = CurrentPrice;
        
        CurrentLeader = bidder;
        CurrentPrice = amount;

        if (BidPlaced != null)
        {
            foreach (var d in BidPlaced.GetInvocationList())
            {
                BidHandler handler = (BidHandler)d;
                try
                {
                    handler(this, new BidEventArgs(bidder, amount));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        if (PreviousLeader != null)
        {
            Outbid?.Invoke(this, new OutbidEventArgs(PreviousLeader, PreviousPrice, amount));
        }
        return true;
    }

    public void Close()
    {
        if (isClosed)
        {
            return;
        }
        else
        {
            isClosed = true;
            Ended?.Invoke(this,new AuctionEndedEventArgs(Item,CurrentLeader,CurrentPrice));
        }
    }
    
    
}