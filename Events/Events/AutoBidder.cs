namespace Events;

public class AutoBidder
{
    public string Name { get; }
    public decimal MaxMoney { get; }
    private Auction _auction;
    
    public AutoBidder(string name, decimal maxMoney, Auction auction)
    {
        Name = name;
        MaxMoney = maxMoney;
        _auction = auction;
        _auction.Outbid += OnOutbid;
    }

    private void OnOutbid(object sender, OutbidEventArgs e)
    {
        if (e.PreviousLeader != Name)
            return;

        if (e.NewAmount + 10 <= MaxMoney)
        {
            _auction.PlaceBid(Name, e.NewAmount + 10);
        }
        else
        {
            Console.WriteLine($"{Name} gives up (limit {MaxMoney})");
            _auction.Outbid -= OnOutbid;
        }
    }
}