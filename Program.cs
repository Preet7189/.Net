using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    // List of diffrent kinds of Rate
    public class TypeList
    {
        public string type { get; set; }
        public string typeFor { get; set; }
    }

    //List of Movies with age restriction
    public class MovieList
    {
        public string Name { get; set; }
        public string Rate { get; set; }
    }

    //class for common things
    public class Common
    {
        // function to make customized heading for perticular sections
        public void heading(string title)
        {
            string s = "****************************************";
            string s2 = "*****Welcome " + title + "******";
            string s3 = "****************************************";

            Console.ForegroundColor = ConsoleColor.Blue;
            //using to display it in the middle
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine(s);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition((Console.WindowWidth - s2.Length) / 2, Console.CursorTop);
            Console.WriteLine(s2);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition((Console.WindowWidth - s3.Length) / 2, Console.CursorTop);
            Console.WriteLine(s3);
            Console.ResetColor();
        }
    }

    //class for admin part
    public class AdminMain 
    {
        string inputPassword;
        List<MovieList> guestMovieList = new List<MovieList>();
        //password of adming procrss
        public int passwordCheckingSecond(List<MovieList> listOfMovies,int count)
        {
            //hardcoded password
            string password = "halfblood";

            if(count == 5)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Please Enter the Admin Password: ");
                Console.ResetColor();
            }
            else if (count == 0)
            {
                return 0;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nYou have " +count+ " more attempts to enter the correct password. \n Press B and hit the Enter to go back to previous screen: ");
                Console.ResetColor();
            }
            inputPassword = Console.ReadLine();

            //if user enters b to go back to main menu
            if(inputPassword.ToLower() == "b")
            {
                return 0;

            } else if(inputPassword == password)
            {
                return 1;
            } else
            {
                //making an instance and counting attemts until 5 
                MovieStarting mllm = new MovieStarting();
                count--;    
                mllm.passwordCheckinFirst(1,listOfMovies,count);
                return 3;
            }
        }

        // for admin to add movies details
        public void askingAboutMovies()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("How many Movies are playing Today?: ");
            Console.ResetColor();

            String[] firstToTenth = new String[10] { "First", "Second", "Third", "Fourth", "Fifth", "Sixth", "Seventh", "Eighth", "Ninth", "Tenth" };
            //type of rates which can be entered by admin
            var rates = new List<TypeList>()
            {
                new TypeList()
                {
                    type = "G",
                    typeFor = ""
                },
                new TypeList()
                {
                    type = "PG",
                    typeFor = "10"
                },
                new TypeList()
                {
                    type = "PG-13",
                    typeFor = "13"
                },
                new TypeList()
                {
                    type = "R",
                    typeFor = "15"
                },
                new TypeList()
                {
                    type = "NC-17",
                    typeFor = "17"
                },

            };

            // checking movienumber is valid or not
            int movieNumber;
            if (int.TryParse(Console.ReadLine(), out movieNumber) && movieNumber <= 10 && movieNumber > 0)
            {
                //getting movie name and its rating 
                for (int i = 0; i < movieNumber; i++)
                {
                    string movieName = "";
                    string rate = "";
                    bool enteringInputMovie = true;
                    bool enteringInputRate = true;
                    Console.WriteLine();
                    while (enteringInputMovie)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Please Enter The " + firstToTenth[i] + "Movie's Name: ");
                        Console.ResetColor();
                        movieName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(movieName))
                        {
                            enteringInputMovie = false;
                        }
                    }
                    while (enteringInputRate)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Please Enter The Age Limit or Rating for the " + firstToTenth[i] + "Movie: ");
                        Console.ResetColor();
                        rate = Console.ReadLine();
                        var isNumeric = int.TryParse(rate, out _);
                        if (!string.IsNullOrEmpty(rate) && ((rate.ToLower() == "g"
                            || rate.ToLower() == "pg"
                            || rate.ToLower() == "pg-13"
                            || rate.ToLower() == "r"
                            || rate.ToLower() == "nc-17") 
                            || isNumeric))
                        {
                            if(isNumeric && ( Int32.Parse(rate) < 1 || Int32.Parse(rate)>120 ))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Please Enter the valid age number, Choose between 1 and 120");
                                Console.ResetColor();
                                continue;
                            }
                            enteringInputRate = false;
                        } else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n\nPlease Enter the age limit or ratings from below list");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("\nG – General Audience, any age is good " + "\n" + "PG – We will take PG as 10 years or older"
                                + "\n" + "PG-13 – We will take PG-13 as 13 years or older" + "\n"+ "R – We will take R as 15 years or older. Don’t worry about accompany by parent case."
                                +"\n"+ "NC-17 – We will take NC-17 as 17 years or older");
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                    }

                    //adding name of movies and ratings via Add 
                    guestMovieList.Add(new MovieList
                    {
                        Name = movieName,
                        Rate = rate
                    });
                    Console.WriteLine();

                    if (i == (movieNumber - 1))
                    {
                        int index = 0;
                        bool untilYorN = true;
                        string YorN;
                        foreach (var movie in guestMovieList.ToArray())
                        {
                            index++;
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine(index + ". " + movie.Name + " {" + movie.Rate + "}");
                            Console.ResetColor();

                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Your Movies Playing Today Are Listed Above. Are You Satisfied? (Y/N)?");
                        Console.ResetColor();
                        YorN = Console.ReadLine();
                        //cheking admin types either y or n
                        while (untilYorN)
                        {
                            if (YorN.ToLower() == "y" || YorN.ToLower() == "n" )
                            {
                                untilYorN = false;
                                if (YorN == "y" || YorN == "Y")
                                {
                                    MovieStarting mm = new MovieStarting();
                                    mm.beginMovie(guestMovieList);
                                }
                                else
                                {
                                    guestMovieList.Clear();
                                    askingAboutMovies();
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Please Enter The Valid Character");
                                Console.ResetColor();
                                YorN = Console.ReadLine();
                                untilYorN = true;
                            }
                        }
                    }   
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You are only allowed to enter the maximum 10 movie or Enter the valid input");
                Console.ResetColor();
                askingAboutMovies();
            }
        }
        //when user enters correct password of admin
        public void successAdmin()
        {
            Common cc = new Common();
            cc.heading("MoviePlex Administrator");
            askingAboutMovies();
        }
    }

    //class for guest section
    public class GuestMain 
    {
        public int GuestSide(List<MovieList> listOfMovies = null)
        {
            Common ccGuest = new Common();
            ccGuest.heading("Guest");
            if(listOfMovies != null)
            {

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("There Are " + listOfMovies.Count + " Movies Playing Today. Please Choose From the Following Movies: ");
                Console.ResetColor();
                for (var i=0; i< listOfMovies.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + listOfMovies[i].Name + "{" + listOfMovies[i].Rate + "}");
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Which Movie Would you like to watch ?");
                Console.ResetColor();
                bool movieWatchVerifying = true;
                int movieWatch = 1;
                //checking user is selecting from list
                while (movieWatchVerifying)
                {
                    if (int.TryParse(Console.ReadLine(), out movieWatch) && movieWatch > 0 && movieWatch <= listOfMovies.Count )
                    {
                        movieWatchVerifying = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nPlease Enter the valid input");
                        Console.ResetColor();
                    }
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Please Enter Your Age For Verification");
                Console.ResetColor();
                int ageVerification = 0;
                bool ageVerifying = true;
                //checking user is putting valid numbers 
                while(ageVerifying)
                {
                    if (int.TryParse(Console.ReadLine(), out ageVerification) && ageVerification > 1 && ageVerification < 120)
                    {
                        ageVerifying = false;
                    } else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nPlease Enter the valid input");
                        Console.ResetColor();
                    }
                }
              
                //checking user is eligible for watching movies
                if(ageVerification > 0 && listOfMovies[movieWatch-1].Rate.ToLower() == "g"
                    || (listOfMovies[movieWatch-1].Rate.ToLower() == "pg" && ageVerification >= 10)
                    || (listOfMovies[movieWatch-1].Rate.ToLower() == "pg-13" && ageVerification >= 13) 
                    || (listOfMovies[movieWatch-1].Rate.ToLower() == "r" && ageVerification >= 15) 
                    || (listOfMovies[movieWatch-1].Rate.ToLower() == "nc-17" && ageVerification >= 17)
                    || (ageVerification >= Int32.Parse(listOfMovies[movieWatch - 1].Rate)))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\n\n Enjoy The Movie!");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Press M to go back to the guest main menu");
                    Console.WriteLine("Press S to go back to the Start page");
                    Console.ResetColor();
                    string inputToGoback = Console.ReadLine();
                    if(inputToGoback.ToLower() == "m")
                    {
                       GuestSide(listOfMovies);
                    } else if (inputToGoback.ToLower() == "s")
                    {
                        return 2;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please only hit the M or S key to go back");
                        Console.ResetColor();
                    }
                } else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You are underage for this movie, please select another movie");
                    Console.ResetColor();
                    GuestSide(listOfMovies);
                    return 55;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No movie Playing Today");
                Console.ResetColor();
            }
            return 55;
        }
    }

    
    class MovieStarting
    {

        AdminMain aa = new AdminMain();
        //password checking first part
        public int passwordCheckinFirst(int select, List<MovieList> listOfMovies = null, int count = 5)
        {
            if (select == 1)
            {
                int passwordChecking = aa.passwordCheckingSecond(listOfMovies,count);
                if (passwordChecking == 0)
                {
                    return 0;
                }
                else if(passwordChecking == 1)
                {
                    Console.WriteLine("\n\n\n");
                    aa.successAdmin();
                } 
            }
            else 
            {
                GuestMain bb = new GuestMain();
                int guestRes = bb.GuestSide(listOfMovies);
                if(guestRes == 2)
                {
                return 2;
                }
            }
             return 0;
           
        }
    
        public int beginMovie(List<MovieList> l = null) 
        {
            
            Common cc2 = new Common();
            cc2.heading("to MoviePlex Theater");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\nPlease select from the following Options");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("1: Administrator");
            Console.WriteLine("2: Guests");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("\n\nSelection: ");
            Console.ResetColor();

            bool selection = true;
            while(selection)
            {
                int select;
                if (int.TryParse(Console.ReadLine(), out select) && (select == 1 || select == 2))
                {
                    selection = false;
                    int finalPassword = passwordCheckinFirst(select, l);
                    if (finalPassword == 0 || finalPassword ==2)
                    {
                        return 0;
                    } 
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("\nPlease Enter The Valid Input or it must be 1 or 2: ");
                    Console.ResetColor();
                }
            }
           return 33;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            runStartUpAgain();
            void runStartUpAgain()
            {
                var instance = new MovieStarting();
                int finalSelect = instance.beginMovie();
              if(finalSelect == 0 )
                {
                    runStartUpAgain();
                }
            } 
        }
    }
}

//Jaivin Movaliya(8726510)
//Preet Patel(8760971)
//Dhruv Patel(8754990)
//Megha Metthew(8753621)
//Purvi Patel(8732983)
