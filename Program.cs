using System;
using System.IO;

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
    Player {gameOver} wins!
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
    Console.WriteLine($"It is player {player}'s turn - Logs in {debug.pathname}");
      List<string> lines  = new List<string>() {};
     int[] nums = {0,3,6};
     foreach(int i in nums) {
      //  Console.Write(i);
      string str = "";
        // Console.Write(lines);
        // Console.Write(j);        
        str += $"{slots[i]} | ";
        str += $"{slots[i+1]} | ";
        str += $"{slots[i+2]}";
        lines.Add(str);
     }
        Console.WriteLine(string.Join("\n---------\n", lines));
    }
    private static void Play() {
    debug.log("void Play#()");
        Console.Write("Choose a place to play: ");
int key = Convert.ToInt32(Console.ReadLine());
if(!(key.ToString() == slots[key])) {
Console.WriteLine("Please enter a valid slot to play");
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
return isX ? 1 : isO ? 2 : 0;
} else if(isOcupied(slots[3]) && isOcupied(slots[4]) && isOcupied(slots[5])) {
string piece = $"{ string.Join("", slots, 3, 3)}"; // all pieces
debug.log($"Checking piece {piece} on 3-4");
Boolean isX = piece == "XXX";
Boolean isO = piece == "OOO";
return isX ? 1 : isO ? 2 : 0;
}  else if(isOcupied(slots[6]) && isOcupied(slots[7]) && isOcupied(slots[8])) {
string piece = $"{ string.Join("", slots, 6, 3)}"; // all pieces
debug.log($"Checking piece {piece} on 6-3");
Boolean isX = piece == "XXX";
Boolean isO = piece == "OOO";
return isX ? 1 : isO ? 2 : 0;
}
else if(isOcupied(slots[0]) && isOcupied(slots[3]) && isOcupied(slots[6])) {
string piece = slots[0]+slots[3]+slots[6]; // all pieces
debug.log($"Checking piece {piece} on 0-3-6");
Boolean isX = piece == "XXX";
Boolean isO = piece == "OOO";
return isX ? 1 : isO ? 2 : 0;
} else if(isOcupied(slots[1]) && isOcupied(slots[4]) && isOcupied(slots[7])) {
string piece = slots[1]+slots[4]+slots[7]; // all pieces
debug.log($"Checking piece {piece} on 1-4-7");
Boolean isX = piece == "XXX";
Boolean isO = piece == "OOO";
return isX ? 1 : isO ? 2 : 0;
} else if(isOcupied(slots[2]) && isOcupied(slots[5]) && isOcupied(slots[8])) {
string piece = slots[2]+slots[5]+slots[8]; // all pieces
debug.log($"Checking piece {piece} on 2-5-8");
Boolean isX = piece == "XXX";
Boolean isO = piece == "OOO";
return isX ? 1 : isO ? 2 : 0;
} else if(isOcupied(slots[2]) && isOcupied(slots[5]) && isOcupied(slots[8])) {
string piece = slots[0]+slots[4]+slots[8]; // all pieces
debug.log($"Checking piece {piece} on 2-5-8");
Boolean isX = piece == "XXX";
Boolean isO = piece == "OOO";
return isX ? 1 : isO ? 2 : 0;
} else if(isOcupied(slots[2]) && isOcupied(slots[5]) && isOcupied(slots[8])) {
string piece = slots[2]+slots[5]+slots[6]; // all pieces
debug.log($"Checking piece {piece} on 2-5-8");
Boolean isX = piece == "XXX";
Boolean isO = piece == "OOO";
return isX ? 1 : isO ? 2 : 0;
}
else {
    return 0;
}

    }
}