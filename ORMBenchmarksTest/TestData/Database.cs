using ORMBenchmarksTest.DTOs;
using ORMBenchmarksTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EntityFramework.BulkInsert.Extensions;

namespace ORMBenchmarksTest.TestData
{
    public static class Database
    {
        public static void Reset()
        {
            using (SportContext context = new SportContext())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE Player");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE Team");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE Sport");
            }
        }

        public static void Load(List<SportDTO> sports, List<TeamDTO> teams, List<PlayerDTO> players)
        {
            AddSports(sports);
            AddTeams(teams);
            AddPlayers(players);
        }

        private static void AddPlayers(List<PlayerDTO> players)
        {
            using (SportContext context = new SportContext())
            {
                List<Player> playersMapped = new List<Player>();
                foreach (var player in players)
                {
                    playersMapped.Add(new Player()
                    {
                        FirstName = player.FirstName,
                        LastName = player.LastName,
                        DateOfBirth = player.DateOfBirth,
                        TeamId = player.TeamId,
                        Id = player.Id
                    });
                }
                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
                context.BulkInsert(playersMapped);
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }
        }

        private static void AddTeams(List<TeamDTO> teams)
        {
            using (SportContext context = new SportContext())
            {
                List<Team> teamsMapped = new List<Team>();
                foreach (var team in teams)
                {
                    teamsMapped.Add(new Team()
                    {
                        Name = team.Name,
                        Id = team.Id,
                        SportId = team.SportId,
                        FoundingDate = team.FoundingDate
                    });
                }
                context.BulkInsert(teamsMapped);

            }
        }

        private static void AddSports(List<SportDTO> sports)
        {
            using (SportContext context = new SportContext())
            {
                List<Sport> sportsMapped = new List<Sport>();
                foreach (var sport in sports)
                {
                    sportsMapped.Add(new Sport()
                    {
                        Id = sport.Id,
                        Name = sport.Name
                    });
                }
                context.BulkInsert(sportsMapped);

            }
        }
    }
}
