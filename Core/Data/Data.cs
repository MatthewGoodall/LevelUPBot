using LevelUP.Resources.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelUP.Core.Data
{
    public static class Data
    {
        public static int GetStones(ulong UserId) {
            using (var DbContext = new SqliteDbContext()) {
                if (DbContext.Users.Where(x => x.UserID == UserId).Count() < 1) return 0;

                return DbContext.Users.Where(x => x.UserID == UserId).Select(x => x.Stones).FirstOrDefault();
            }
        }

        public static async Task SaveStones(ulong UserId, int Amount) {
            using (var DbContext = new SqliteDbContext()) {

                if (DbContext.Users.Where(x => x.UserID == UserId).Count() < 1)
                {
                    
                    DbContext.Users.Add(new User
                    {
                        UserID = UserId,
                        Stones = Amount,
                        Level = 1,
                        Experience = 0,
                    });
                }
                else {
                    User Current = DbContext.Users.Where(x => x.UserID == UserId).FirstOrDefault();
                    Current.Stones += Amount;
                    DbContext.Users.Update(Current);
                }
                await DbContext.SaveChangesAsync();
            }
        }

        public static int GetLevel(ulong UserId) {
            using (var DbContext = new SqliteDbContext()) {
                if (DbContext.Users.Where(x => x.UserID == UserId).Count() < 1) {
                    DbContext.Users.Add(new User
                    {
                        UserID = UserId,
                        Stones = 0,
                        Level = 1,
                        Experience = 0,
                    });
                }

                return DbContext.Users.Where(x => x.UserID == UserId).Select(x => x.Level).FirstOrDefault();
            }
        }

        public static async Task SaveLevel(ulong UserId) {
            using (var DbContext = new SqliteDbContext()) {
                if (DbContext.Users.Where(x => x.UserID == UserId).Count() < 1) return;

                User Current = DbContext.Users.Where(x => x.UserID == UserId).FirstOrDefault();
                Current.Level += 1;
                DbContext.Users.Update(Current);
                await DbContext.SaveChangesAsync();
            }
        } 

        public static int GetExperience(ulong UserId) {
            using (var DbContext = new SqliteDbContext()) {
                if (DbContext.Users.Where(x => x.UserID == UserId).Count() < 1) return 0;
                return DbContext.Users.Where(x => x.UserID == UserId).Select(x => x.Experience).FirstOrDefault();
            }
        }

        public static async Task SaveExperience(ulong UserId, int Experience = 10) {
            using (var DbContext = new SqliteDbContext()) {
                if (DbContext.Users.Where(x => x.UserID == UserId).Count() < 1)
                {
                    DbContext.Users.Add(new User
                    {
                        UserID = UserId,
                        Stones = 0,
                        Level = 1,
                        Experience = 0,
                    });
                }
                else {
                    User Current = DbContext.Users.Where(x => x.UserID == UserId).FirstOrDefault();
                    Current.Experience += Experience;
                    int Level = (int)(MathF.Floor(25 + MathF.Sqrt(625 + 100 * Current.Experience)) / 50);
                    if (Level != Current.Level) Current.Level += 1;
                    DbContext.Users.Update(Current);
                }
                await DbContext.SaveChangesAsync();
            }
            
        }
    }
}
