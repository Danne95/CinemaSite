using Project.Dal;
using Project.Models;
using Project.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.x = "5";
            return View();
        }

        public ActionResult About(string sortBy = "",string date ="",string catagory="",string time="",float price = 0,bool sale=false)
        {
            ViewBag.sort = sortBy;
            ViewBag.date= date;
            ViewBag.catagory = catagory;
            ViewBag.time = time;
            ViewBag.price = price;
            MovieListDal dal = new MovieListDal();
            movieListViewModel viewModel = new movieListViewModel();
            List<MovieList> movies = dal.movies.ToList<MovieList>();
            viewModel.movie = new MovieList();
            viewModel.movies = movies;

            if (sortBy.Equals("priceInc"))
            {
                for (int j = 0; j <= movies.Count - 2; j++)
                {
                    for (int i = 0; i <= movies.Count - 2; i++)
                    {
                        if ((movies[i].price - movies[i].price * movies[i].sale) > (movies[i + 1].price - movies[i + 1].price * movies[i + 1].sale))
                        {
                            MovieList temp = movies[i + 1];
                            movies[i + 1] = movies[i];
                            movies[i] = temp;
                        }
                    }
                }

            }
            if (sortBy.Equals("priceDec"))
            {
                for (int j = 0; j <= movies.Count - 2; j++)
                {
                    for (int i = 0; i <= movies.Count - 2; i++)
                    {
                        if ((movies[i].price - movies[i].price * movies[i].sale) < (movies[i + 1].price - movies[i + 1].price * movies[i + 1].sale))
                        {
                            MovieList temp = movies[i + 1];
                            movies[i + 1] = movies[i];
                            movies[i] = temp;
                        }
                    }
                }

            }
            if (sortBy.Equals("rating"))
            {
                for (int j = 0; j <= movies.Count - 2; j++)
                {
                    for (int i = 0; i <= movies.Count - 2; i++)
                    {
                        if ((movies[i].rating) < (movies[i + 1].rating))
                        {
                            MovieList temp = movies[i + 1];
                            movies[i + 1] = movies[i];
                            movies[i] = temp;
                        }
                    }
                }

            }
            if (sortBy.Equals("category"))
            {
                for (int j = 0; j <= movies.Count - 2; j++)
                {
                    for (int i = 0; i <= movies.Count - 2; i++)
                    {
                        if (String.Compare(movies[i].category,movies[i+1].category)>0)
                        {
                            MovieList temp = movies[i + 1];
                            movies[i + 1] = movies[i];
                            movies[i] = temp;
                        }
                    }
                }

            }
            if (!date.Equals(""))
            {
                for(int i = 0; i < movies.Count; i++)
                {
                    if (!movies[i].streamDate.Equals(date))
                    {
                        movies.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (!catagory.Equals(""))
            {
                for (int i = 0; i < movies.Count; i++)
                {
                    if (!movies[i].category.Equals(catagory))
                    {
                        movies.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (!time.Equals(""))
            {
                for (int i = 0; i < movies.Count; i++)
                {
                    if (!movies[i].streamTime.Equals(time))
                    {
                        movies.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (price!=0)
            {
                for (int i = 0; i < movies.Count; i++)
                {
                    if (movies[i].price!=price)
                    {
                        movies.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (sale)
            {
                for (int i = 0; i < movies.Count; i++)
                {
                    if (movies[i].sale==0)
                    {
                        movies.RemoveAt(i);
                        i--;
                    }
                }
            }
            return View(viewModel);

        }
        public ActionResult Contact(string username=null,string password=null ,bool authorized=false)
        {
            ViewBag.username = username;
            ViewBag.password = password;
            ViewBag.authorized = authorized;
            ViewBag.massege = "";

            accountListDal dal = new accountListDal();
            accountListViewModel viewModel = new accountListViewModel();
            List<accountList> accounts = dal.accounts.ToList<accountList>();
            if (username != null && password != null)
            {
                foreach (accountList account in accounts)
                {
                    if (account.username.Equals(username))
                    {
                        if (account.password.Equals(password))
                        {
                            ViewBag.authorized = true;
                            return RedirectToAction("ManageMovies", "Home");
                        }
                        else
                        {
                            ViewBag.massege = "Wrong password, Please try again";
                        }
                        break;
                    }
                }
                if (ViewBag.massege.Equals("")) 
                { 
                    ViewBag.massege = "Username doesn't exists";

                }
            }

            return View();
        }

        public ActionResult ManageMovies(string movieName="",string streamTime = "", string streamDate = "", string hall = "", string olhall = "", string bull = "", string moviePrice = "", string sale = "",string link="",string age = "",string cat="")
        {
            ViewBag.msg = "";
            ViewBag.price = moviePrice;
            ViewBag.sale = null;
            ViewBag.hall = hall;
            ViewBag.name = movieName;
            ViewBag.date = streamDate;
            ViewBag.time = streamTime;
            ViewBag.bul = bull;
            ViewBag.t = null;
            ViewBag.link = "";
            ViewBag.age = "";
            ViewBag.cat = "";
            
            MovieListDal dal = new MovieListDal();
            movieListViewModel viewModel = new movieListViewModel();
            List<MovieList> movies = dal.movies.ToList<MovieList>();
            viewModel.movie = new MovieList();
            viewModel.movies = movies;
            MovieList movieToChange = null;

            if (bull.Equals("deletee")) 
            {
                if (movieToChange != null)
                {
                    dal.movies.Remove(movieToChange);
                    dal.SaveChanges();
                    return RedirectToAction("ManageMovies", "Home");

                }
            }
            if (bull.Equals("edit"))
            {
                if (!moviePrice.Equals("") || !sale.Equals(""))
                {
                    foreach (MovieList movi1e in movies)
                    {
                    
                        if (movi1e.streamTime.Equals(streamTime))
                        {
                            if (movi1e.movieName.Equals(movieName))
                            {

                                if (movi1e.streamDate.Equals(streamDate))
                                {
                                    if (movi1e.hall.Equals(olhall))
                                    {
                                        ViewBag.t = movi1e;
                                        movieToChange = movi1e;
                                    }

                                }
                            }
                        }
                    }
                }
                if (movieToChange != null)
                {
                    dal.movies.Remove(movieToChange);
                    dal.SaveChanges();
                    movieToChange.price = float.Parse(moviePrice);
                    movieToChange.sale = float.Parse(sale);
                    movieToChange.hall = hall;
                    dal.movies.Add(movieToChange);
                    dal.SaveChanges();
                    return RedirectToAction("ManageMovies", "Home");
                }
            }
            if (bull.Equals("add"))
            {
                if (!movieName.Equals(""))
                {
                    if (!streamTime.Equals(""))
                    {
                        if (!streamDate.Equals(""))
                        {
                            if (!link.Equals(""))
                            {
                                if (!hall.Equals(""))
                                {
                                    if (!moviePrice.Equals(""))
                                    {
                                        if (!streamTime.Equals(""))
                                        {
                                            MovieList movieToAdd = new MovieList();
                                            movieToAdd.movieName = movieName;
                                            string[] strr = streamTime.Split(':');
                                            string newstrr = strr[0] + ":" + strr[1];
                                            movieToAdd.streamTime = newstrr;
                                            string []str = streamDate.Split('-');
                                            string newstr = str[2] +"/"+ str[1] + "/" + str[0];
                                            movieToAdd.streamDate = newstr;
                                            movieToAdd.rating = 0;
                                            movieToAdd.imageLink = link;
                                            movieToAdd.hall = hall;
                                            movieToAdd.price = float.Parse( moviePrice);
                                            movieToAdd.seats = 50;
                                            movieToAdd.takenSeats = ",";
                                            if (age.Equals(""))
                                                age = "No age limit";
                                            if (sale.Equals(""))
                                                sale = "0";
                                            movieToAdd.sale = float.Parse(sale);
                                            movieToAdd.ageLim = age;
                                            movieToAdd.category = cat;
                                            dal.movies.Add(movieToAdd);
                                            dal.SaveChanges();
                                            return RedirectToAction("ManageMovies", "Home");
                                        }
                                        else
                                        {
                                            ViewBag.msg = "Please enter new movie catagory";
                                        }

                                    }
                                    else
                                    {
                                        ViewBag.msg = "Please enter new movie price";
                                    }

                                }
                                else
                                {
                                    ViewBag.msg = "Please enter new movie hall";
                                }
                            }
                            else
                            {
                                ViewBag.msg = "Please enter new movie image link";

                            }

                        }
                        else
                        {
                            ViewBag.msg = "Please enter new movie streaming date";
                        }

                    }
                    else
                    {
                        ViewBag.msg = "Please enter new movie streaming time";
                    }
                }
                else
                {
                    ViewBag.msg = "Please enter name";
                }

            }
            //if (ViewBag.t) 
            //{
            //    return RedirectToAction("ManageMovies", "Home",new { ViewBag.name, ViewBag.time, ViewBag.date, ViewBag.moviePrice, ViewBag.sale, ViewBag.hall });
            //}
            //if (moviePrice != 0 || sale != 0 || !hall.Equals(""))
            //{

            //    foreach (MovieList movi1e in movies)
            //    {

            //        if (movi1e.streamTime.Equals(streamTime))
            //        {
            //            ViewBag.msg = "HERE1";
            //            if (movi1e.movieName.Equals(movieName))
            //            {
            //                ViewBag.msg = "HERE2";

            //                if (movi1e.streamDate.Equals(streamDate))
            //                {
            //                    ViewBag.msg = "HERE3";

            //                    movieToChange = movi1e;

            //                }
            //            }
            //        }
            //    }
            //}
            //if (movieToChange != null)
            //{
            //    dal.movies.Remove(movieToChange);
            //    dal.SaveChanges();
            //    movieToChange.price = moviePrice;
            //    movieToChange.sale = sale;
            //    movieToChange.hall = hall;
            //    dal.movies.Add(movieToChange);
            //    dal.SaveChanges();
            //}

            return View(viewModel);
        }

        public ActionResult BuyTickets(string movieName, string seats = "")
        {
            ViewBag.seats = seats;
            ViewBag.name = movieName;
            ViewBag.count = 0;
            

            MovieListDal dal = new MovieListDal();
            movieListViewModel viewModel = new movieListViewModel();
            List<MovieList> movies = dal.movies.ToList<MovieList>();
            viewModel.movie = new MovieList();
            viewModel.movies = movies;
            ViewBag.Message = movieName;
            

            return View(viewModel);
        }

        public ActionResult Payment(string movieName,string chosenSeats,int count)
        {
            ViewBag.seat = chosenSeats;
            ViewBag.movie = movieName;
            ViewBag.count = count;
            MovieListDal dal = new MovieListDal();
            movieListViewModel viewModel = new movieListViewModel();
            List<MovieList> movies = dal.movies.ToList<MovieList>();
            viewModel.movie = new MovieList();
            viewModel.movies = movies;
            MovieList movie=null;
            foreach (MovieList movi1e in movies)
            {
                if (movi1e.movieName.Equals(movieName))
                {
                    movie = movi1e;
                }
            }
            //MovieList movie = dal.movies.Find(movieName);
            string[] takenSeatsArr = movie.takenSeats.Split(',');
            string[] chosenSeatsArr = chosenSeats.Split(',');
            chosenSeatsArr = chosenSeatsArr.Distinct().ToArray();
            dal.movies.Remove(movie);
            dal.SaveChanges();
            foreach(string c in chosenSeatsArr)
            {
                if(!c.Equals("")||!c.Equals(","))
                    movie.takenSeats = movie.takenSeats + "," + c;
            }
            movie.takenSeats = movie.takenSeats.Replace(",,", ",");
            dal.movies.Add(movie);
            dal.SaveChanges();
            return View(viewModel);

        }

        public ActionResult CancelPayment(string movieName, string chosenSeats)
        {
            ViewBag.movie = movieName;
            MovieListDal dal = new MovieListDal();
            movieListViewModel viewModel = new movieListViewModel();
            List<MovieList> movies = dal.movies.ToList<MovieList>();
            viewModel.movie = new MovieList();
            viewModel.movies = movies;
            ViewBag.Message = movieName;
            MovieList movie = null;
            foreach (MovieList movi1e in movies)
            {
                if (movi1e.movieName.Equals(movieName))
                {
                    movie = movi1e;
                }
            }
            //MovieList movie = dal.movies.Find(movieName);
            string[] takenSeatsArr = movie.takenSeats.Split(',');
            string[] chosenSeatsArr = chosenSeats.Split(',');
            string newTaken = ",";
            dal.movies.Remove(movie);
            dal.SaveChanges();
            foreach(string c in takenSeatsArr)
            {
                if (!chosenSeatsArr.Contains(c))
                    newTaken = newTaken + c + ",";
            }
            movie.takenSeats = newTaken;
            dal.movies.Add(movie);
            dal.SaveChanges();
            return View();
        }
        public ActionResult ThankYou()
        {

            return View();
        }

    }
}