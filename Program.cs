using System;
using System.IO;
class Colors {
   public static string red(string str) {
        return $"{FgRed}{str}{Reset}";
    }
 public static string   Reset = "\x1b[0m";
public static string Bright = "\x1b[1m";
public static string Dim = "\x1b[2m";
public static string Underscore = "\x1b[4m";
public static string Blink = "\x1b[5m";
public static string Reverse = "\x1b[7m";
public static string Hidden = "\x1b[8m";
public static string FgBlack = "\x1b[30m";
public  static string FgRed = "\x1b[31m";
public static string FgGreen = "\x1b[32m";
public static string FgYellow = "\x1b[33m";
public static string FgBlue = "\x1b[34m";
public static string FgMagenta = "\x1b[35m";
public static string FgCyan = "\x1b[36m";
public static string FgWhite = "\x1b[37m";
public static string BgBlack = "\x1b[40m";
public static string BgRed = "\x1b[41m";
public static string BgGreen = "\x1b[42m";
public static string BgYellow = "\x1b[43m";
public static string BgBlue = "\x1b[44m";
public static string BgMagenta = "\x1b[45m";
public static string BgCyan = "\x1b[46m";
public static string BgWhite = "\x1b[47m";
}
class Debugger {
public Boolean  initalised = false;
public Boolean created = false;
public List<string> logs = new List<string>();
public string  pathname = "";
public void init() {
    DateTime now = DateTime.Now;
    string parent = default!;
    Console.WriteLine();
    if(Array.Exists(Environment.GetCommandLineArgs(), element => element.ToLower() == "dev" || element == "--dev"))
        parent = "logs";
    else 
parent = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.tictactoeapp";
if(!Directory.Exists(parent + "/")) {
Directory.CreateDirectory(parent + "/");
}
pathname = $"{parent}/log-{now.Day}-{now.Month}-{now.Year}-{now.Hour}.log";
created = File.Exists(pathname);
   if(!created) File.Create(pathname).Close();
    foreach (string line in File.ReadAllLines(pathname)) {
        logs.Add(line);
    }
    initalised = true;

}
public void log(string message) {
// logFile.Write("[DEBUG] " + message);
logs.Add($"[DEBUG] - {message}");
SaveFile();
}
private void SaveFile() {
    string logstr =  string.Join("\n", logs);
    File.WriteAllText(pathname, logstr);

}
}
 class Program {
    static string[] slots = {"0","1","2","3","4","5","6","7","8","9"};
    static int player = 1;
    static Debugger debug = new Debugger();
    static int gameOver = 0;
    static int[] winnerSlots = {};
    static DateTime startTime = default!;
    public static void Main() {    
if(debug.initalised) {
        DateTime Time = DateTime.Now;
        TimeSpan nowTime = Time.Subtract(startTime);
    Console.Title = $"Tic Tac Toe - Time: {nowTime.Hours}:{nowTime.Minutes} ";
    debug.log("Main#");

} else {
    debug.init();
debug.log("Loaded");
if(Array.Exists(Environment.GetCommandLineArgs(), e => e == "--dump-logs")) {
    foreach(string log in debug.logs) {
        Console.WriteLine(log);
    } 
    Console.WriteLine("\n Full logs path: " + debug.pathname);
    System.Environment.Exit(0);
}
    startTime = DateTime.Now;
}   
Run();
    }
private static void Run() {
    debug.log("void Run#()");
if(gameOver != 0) {
   // Console.Clear();
   Board();
   Console.WriteLine(
    $@"
{Colors.BgGreen+ "                                                      " +Colors.Reset}
                  {Colors.FgGreen} Player {gameOver} wins! {Colors.Reset}
{Colors.BgGreen+ "                                                      " +Colors.Reset}
    ");
    System.Environment.Exit(0);
    return;
} else {
//Console.Clear();
   Board();
Play();
Main(); 
}

}
    private static void Board() {
    debug.log("void board#()");
    Console.WriteLine(Colors.red($"It is player {player}'s turn - Logs in {debug.pathname}"));
      List<string> lines  = new List<string>() {};
     int[] nums = {0,3,6};
     foreach(int i in nums) {
      //  Console.Write(i);
      string str = "";
        // Console.Write(lines);
        // Console.Write(j);     
       string slot1= slots[i] == "X" ? Colors.FgBlack + "X" + Colors.Reset : slots[i] == "O" ? Colors.FgWhite + "O"+Colors.Reset : Colors.Dim + slots[i] + Colors.Reset; 
       string slot2=  slots[i+1] == "X" ? Colors.FgBlack + "X" + Colors.Reset : slots[i+1] == "O" ? Colors.FgWhite + "O"+Colors.Reset : Colors.Dim + slots[i+1] + Colors.Reset;
       string slot3=  slots[i+2] == "X" ? Colors.FgBlack + "X" + Colors.Reset : slots[i+2] == "O" ? Colors.FgWhite + "O"+Colors.Reset : Colors.Dim + slots[i+2] + Colors.Reset;
        string bar = Colors.Dim + " | " + Colors.Reset;
        str +=   $"{slot1}{bar} " + Colors.Reset;
        str +=   $"{slot2}{bar} " +Colors.Reset;
        str +=  $"{slot3}" +Colors.Reset;
        lines.Add(str);
     }
        Console.WriteLine(string.Join(Colors.Dim + "\n---------\n"+Colors.Reset, lines));
    }
    private static void Play() {
    debug.log("void Play#()");
        Console.Write(Colors.Dim + "Choose a place to play: "+Colors.Reset);
int key = Convert.ToInt32(Console.ReadLine());
if(!(key.ToString() == slots[key])) {
Console.WriteLine(Colors.red("Please enter a valid slot to play"));
Play();
return;
}
slots[key] = player == 1 ? "X" : "O" ;
int gameWinner = checkBoard();
if(gameWinner == 0) {
switchPlayer();
} else {
    gameOver = gameWinner;
}
    } 
    private static void switchPlayer() {
        player = player == 1 ? 2 : 1;
    }
   private static Boolean isOcupied(string piece) {
    return piece == "O" || piece == "X";
   }
    private static int checkBoard() {
if(isOcupied(slots[0]) && isOcupied(slots[1]) && isOcupied(slots[2])) {
string piece = $"{string.Join("", slots, 0, 3)}"; // all pieces
Boolean isX = piece == "XXX";
Boolean isO = piece == "OOO";
if(isX || isO) return isX ? 1 :isO ? 2 :  -1;
} 
 if(isOcupied(slots[3]) && isOcupied(slots[4]) && isOcupied(slots[5])) {
string piece = $"{ string.Join("", slots, 3, 3)}"; // all pieces
debug.log($"Checking piece {piece} on 3-4");
Boolean isX = piece == "XXX";
Boolean isO = piece == "OOO";
if(isX || isO) return isX ? 1 :isO ? 2 :  -1;
}   if(isOcupied(slots[6]) && isOcupied(slots[7]) && isOcupied(slots[8])) {
string piece = $"{ string.Join("", slots, 6, 3)}"; // all pieces
debug.log($"Checking piece {piece} on 6-3");
Boolean isX = piece == "XXX";
Boolean isO = piece == "OOO";
if(isX || isO) return isX ? 1 :isO ? 2 :  -1;
}
 if(isOcupied(slots[0]) && isOcupied(slots[3]) && isOcupied(slots[6])) {
string piece = slots[0]+slots[3]+slots[6]; // all pieces
debug.log($"Checking piece {piece} on 0-3-6");
Boolean isX = piece == "XXX";
Boolean isO = piece == "OOO";
if(isX || isO) return isX ? 1 :isO ? 2 :  -1;
}  if(isOcupied(slots[1]) && isOcupied(slots[4]) && isOcupied(slots[7])) {
string piece = slots[1]+slots[4]+slots[7]; // all pieces
debug.log($"Checking piece {piece} on 1-4-7");
Boolean isX = piece == "XXX";
Boolean isO = piece == "OOO";
if(isX || isO) return isX ? 1 :isO ? 2 :  -1;
} if(isOcupied(slots[2]) && isOcupied(slots[4]) && isOcupied(slots[6])) {
string piece = slots[2]+slots[4]+slots[6]; // all pieces
debug.log($"Checking piece {piece} on 2-4-6");
Boolean isX = piece == "XXX";
Boolean isO = piece == "OOO";
if(isX || isO) return isX ? 1 :isO ? 2 :  -1;
}  if(isOcupied(slots[0]) && isOcupied(slots[4]) && isOcupied(slots[8])) {
string piece = slots[0]+slots[4]+slots[8]; // all pieces
debug.log($"Checking piece {piece} on 0-4-8");
Boolean isX = piece == "XXX";
Boolean isO = piece == "OOO";
if(isX || isO) return isX ? 1 :isO ? 2 :  -1;
}
    return 0;


    }
}