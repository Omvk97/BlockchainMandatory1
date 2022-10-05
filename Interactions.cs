using CommandLine;

namespace BitcoinCoreInteraction;

public class Interactions
{
    [Verb("newblockchain", HelpText = "Should be run once")]
    public class NewBlockchain
    {
        
    }
    [Verb("checkbalance", HelpText = "Check the balance of the associated wallet")]
    public class CheckBalance
    {
        
    }
    
    [Verb("newaddress")]
    public class NewAddress {}
    
    [Verb("sendToAddress")]
    public class SendToAddress
    {
        [Option('a', "address", Required = true, HelpText = "The address to sned to")]
        public string Address { get; set; }
        [Option("amount", Required = true, HelpText = "Amount of blocks to send")]
        public int Blocks { get; set; }
    }
    
    [Verb("listunspendtransactions")]
    public class ListUnspendTransactions
    {
        
    }
}