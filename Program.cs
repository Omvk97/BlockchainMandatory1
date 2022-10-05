using BitcoinCoreInteraction;
using CommandLine;

Parser.Default
    .ParseArguments<Interactions.NewBlockchain, Interactions.CheckBalance, Interactions.NewAddress, Interactions.SendToAddress,
        Interactions.ListUnspendTransactions>(args)
    .MapResult(
        (Interactions.NewBlockchain opts) =>
        {
            ExecuteCommand("docker rm -f bitcoin-server", false);
            Console.WriteLine("Removed previous server, if any");
            ExecuteCommand("docker run --rm --name bitcoin-server ruimarinho/bitcoin-core -regtest=1");
            ExecuteCommand("docker exec --user bitcoin bitcoin-server bitcoin-cli -regtest createwallet 'wallet'");
            Console.Write("Block chain created");
            return 0;
        },
        (Interactions.CheckBalance opts) =>
            ExecuteCommand("docker exec --user bitcoin bitcoin-server bitcoin-cli -regtest getbalance"),
        (Interactions.NewAddress opts) =>
            ExecuteCommand("docker exec --user bitcoin bitcoin-server bitcoin-cli -regtest getnewaddress"),
        (Interactions.SendToAddress opts) =>
            ExecuteCommand(
                $"docker exec --user bitcoin bitcoin-server bitcoin-cli -regtest generatetoaddress {opts.Blocks} {opts.Address}"),
        (Interactions.ListUnspendTransactions opts) =>
            ExecuteCommand($"docker exec --user bitcoin bitcoin-server bitcoin-cli -regtest listunspent"),
        errs => 1
    );
        
static int ExecuteCommand(string command, bool writeOutput = true)
{
     var proc = new System.Diagnostics.Process ();
     proc.StartInfo.FileName = "/bin/bash";
     proc.StartInfo.Arguments = "-c \" " + command + " \"";
     proc.StartInfo.UseShellExecute = false; 
     proc.StartInfo.RedirectStandardOutput = true;
     proc.Start();
     
     while (!proc.StandardOutput.EndOfStream) {
         if (writeOutput) Console.WriteLine (proc.StandardOutput.ReadLine());
     }

     return 0;
}