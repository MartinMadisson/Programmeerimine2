
using System;
using System.Linq;
using KooliProjekt.Application.Authentication;
using KooliProjekt.Application.Data.Football_prediction_MartinM;

namespace KooliProjekt.Application.Data
{
    public static class SeedData
    {
        public static void Generate(ApplicationDbContext context)
        {
            // 1. TEAMS - 35 Eesti jalgpalliklubi

            if (!context.Teams.Any())
            {
                string[] teamNames =
                {
                    "FC Flora Tallinn",
                    "FCI Levadia Tallinn",
                    "Nõmme Kalju FC",
                    "Paide Linnameeskond",
                    "JK Narva Trans",
                    "Pärnu JK Vaprus",
                    "FC Kuressaare",
                    "Tartu JK Tammeka",
                    "Tallinna JK Kalev",
                    "Harju JK Laagri",
                    "FC Elva",
                    "Viimsi JK",
                    "FC Tallinn",
                    "Tallinna FCI Levadia U21",
                    "Tallinna FC Flora U21",
                    "Paide Linnameeskond U21",
                    "Tartu JK Welco",
                    "JK Tabasalu",
                    "FC Nõmme United",
                    "JK Tallinna Kalev U21",
                    "JK Sillamäe Kalev",
                    "Kohtla-Järve JK Järve",
                    "Rakvere JK Tarvas",
                    "Viljandi JK Tulevik",
                    "Pärnu JK",
                    "Läänemaa JK",
                    "Raplamaa JK",
                    "JK Tervis Pärnu",
                    "Tartu FC Helios",
                    "Tallinna FC Zapoos",
                    "FC Kose",
                    "JK Poseidon",
                    "FC Hiiumaa",
                    "Saue JK",
                    "Keila JK"
                };

                foreach (var name in teamNames)
                {
                    context.Teams.Add(new Team
                    {
                        Name = name
                    });
                }

                context.SaveChanges();
            }

            // 2. TOURNAMENTS - 35 turniiri

            if (!context.Tournaments.Any())
            {
                string[] competitions =
                {
                    "Premium liiga",
                    "Esiliiga",
                    "Esiliiga B",
                    "Teine liiga",
                    "Kolmas liiga",
                    "Neljas liiga",
                    "Eesti karikavõistlused"
                };

                for (int i = 0; i < 35; i++)
                {
                    int year = 2022 + i / competitions.Length;

                    context.Tournaments.Add(new Tournament
                    {
                        Name = $"{competitions[i % competitions.Length]} {year}"
                    });
                }

                context.SaveChanges();
            }

            // 3. PLAYERS - 35 väljamõeldud Eesti mängijat

            if (!context.Players.Any())
            {
                string[] firstNames =
                {
                    "Martin", "Karl", "Markus", "Rasmus", "Henri",
                    "Kristjan", "Oliver", "Robert", "Kevin", "Andreas"
                };

                string[] lastNames =
                {
                    "Tamm", "Saar", "Kask", "Sepp"
                };

                for (int i = 0; i < 35; i++)
                {
                    context.Players.Add(new Player
                    {
                        Name = firstNames[i % firstNames.Length]
                            + " " + lastNames[i / firstNames.Length]
                    });
                }

                context.SaveChanges();
            }

            // 4. USERS - 35 testkasutajat

            if (!context.Users.Any())
            {
                // Üks räsi kõigile testkasutajatele (räsimine on tahtlikult aeglane)
                var testPasswordHash = PasswordHelper.Hash("TEST_ONLY_NOT_A_REAL_PASSWORD");

                for (int i = 1; i <= 35; i++)
                {
                    context.Users.Add(new User
                    {
                        username = $"jalkafann{i}",
                        password = testPasswordHash
                    });
                }

                context.SaveChanges();
            }

            // 5. MATCHES - 35 jalgpallimängu

            if (!context.Matches.Any())
            {
                var teams = context.Teams
                    .OrderBy(t => t.Id)
                    .ToList();

                var tournaments = context.Tournaments
                    .OrderBy(t => t.Id)
                    .ToList();

                if (teams.Count >= 2 && tournaments.Count > 0)
                {
                    for (int i = 0; i < 35; i++)
                    {
                        context.Matches.Add(new Match
                        {
                            Date = new DateTime(2026, 4, 1).AddDays(i * 2),
                            TournamentId =
                                tournaments[i % tournaments.Count].Id,
                            Teamt1Id =
                                teams[i % teams.Count].Id,
                            Team2Id =
                                teams[(i + 1) % teams.Count].Id,
                            Team1Score = i % 4,
                            Team2Score = (i + 2) % 4,
                            RoundId = (i / 10) + 1
                        });
                    }

                    context.SaveChanges();
                }
            }

            // 6. PREDICTIONS - 35 mänguennustust

            if (!context.Predictions.Any())
            {
                var matches = context.Matches
                    .OrderBy(m => m.Id)
                    .ToList();

                var users = context.Users
                    .OrderBy(u => u.id)
                    .ToList();

                if (matches.Count > 0 && users.Count > 0)
                {
                    for (int i = 0; i < 35; i++)
                    {
                        context.Predictions.Add(new Prediction
                        {
                            MatchId = matches[i % matches.Count].Id,
                            UserId = users[i % users.Count].id,
                            Team1Score = i % 4,
                            Team2Score = (i + 1) % 4,
                            Points = i % 4
                        });
                    }

                    context.SaveChanges();
                }
            }

            // 7. SCOREBOARDS - 35 edetabelikirjet

            if (!context.Scoreboards.Any())
            {
                var users = context.Users
                    .OrderBy(u => u.id)
                    .ToList();

                var tournaments = context.Tournaments
                    .OrderBy(t => t.Id)
                    .ToList();

                if (users.Count > 0 && tournaments.Count > 0)
                {
                    for (int i = 0; i < 35; i++)
                    {
                        context.Scoreboards.Add(new Scoreboard
                        {
                            UserId = users[i % users.Count].id,
                            TournamentId =
                                tournaments[i % tournaments.Count].Id,
                            Score = (i + 1) * 3
                        });
                    }

                    context.SaveChanges();
                }
            }

            EnsureLoginUsers(context);
        }

        // Ülesanded 12-14: tagab, et andmebaasis on sisselogimiseks kaks kasutajat
        //   admin    / Admin123!    (IsAdmin = true)
        //   kasutaja / Kasutaja123! (IsAdmin = false)
        // Lisaks parandab vanad kasutajad, kelle parool on veel tavalise tekstina.
        private static void EnsureLoginUsers(ApplicationDbContext context)
        {
            // Vanad (räsimata) paroolid -> räsiks, muidu ei saaks nendega sisse logida
            var legacyUsers = context.Users.ToList()
                .Where(u => !PasswordHelper.IsHash(u.password))
                .ToList();

            if (legacyUsers.Count > 0)
            {
                var legacyHashes = new System.Collections.Generic.Dictionary<string, string>();
                foreach (var user in legacyUsers)
                {
                    if (!legacyHashes.TryGetValue(user.password, out var hash))
                    {
                        hash = PasswordHelper.Hash(user.password);
                        legacyHashes[user.password] = hash;
                    }

                    user.password = hash;
                }

                context.SaveChanges();
            }

            if (!context.Users.Any(u => u.username == "admin"))
            {
                context.Users.Add(new User
                {
                    username = "admin",
                    password = PasswordHelper.Hash("Admin123!"),
                    IsAdmin = true
                });
            }

            if (!context.Users.Any(u => u.username == "kasutaja"))
            {
                context.Users.Add(new User
                {
                    username = "kasutaja",
                    password = PasswordHelper.Hash("Kasutaja123!"),
                    IsAdmin = false
                });
            }

            context.SaveChanges();
        }
    }
}
