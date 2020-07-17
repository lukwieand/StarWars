using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace StarWars.Data
{
    public class DBService : IDBService
    {
        /// Database connection
        private readonly SqlConnectionConfiguration _configuration;

        public DBService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }


        // add (create)
        public async Task<bool> CharacterInsert(Character characters)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Name", characters.Name, DbType.String);

                const string query = @"INSERT INTO Character (Name) VALUES  (@Name)";
                await conn.ExecuteAsync(query, parameters , commandType: CommandType.Text);
            }
            return true;
        }

        // list
        public async Task<IEnumerable<Character>> CharacterList()
        {
            IEnumerable<Character> characters;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"SELECT CharacterId, Name FROM Character";
                characters = await conn.QueryAsync<Character>(query, commandType: CommandType.Text);
            }
            return characters;
        }

        /// GetOne
        
        public async Task<Character> CharacterGetOne(int characterid)
        {
            Character character = new Character();
            var parameters = new DynamicParameters();

            parameters.Add("CharacterId", characterid, DbType.Int32);
         

            using (var conn = new SqlConnection(_configuration.Value))
            {              

                const string query = @"SELECT CharacterId, Name FROM Character WHERE (CharacterId = @Characterid) ";
                character = await conn.QueryFirstOrDefaultAsync<Character> (query, parameters, commandType: CommandType.Text);
            }
            return character;
        }


        ///// Update
        public async Task<bool> CharacterUpdate(Character characters)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("CharacterId", characters.CharacterId, DbType.Int32 );
                parameters.Add("Name", characters.Name, DbType.String);

                const string query = @"UPDATE Character SET Name = @Name WHERE (CharacterId = @CharacterId) ";
                await conn.ExecuteAsync(query, parameters, commandType: CommandType.Text);
            }
            return true;
        }


        /// Delete
        /// 
        public async Task<bool> CharacterDelete(int characterid)
        {      
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("CharacterId", characterid, DbType.Int32);
                const string query = @"DELETE FROM Character WHERE (CharacterId = @Characterid) ";
                await conn.ExecuteAsync(query, parameters, commandType: CommandType.Text);
            }
            return true;
        }


        ////// FRIENDS

        public async Task<IEnumerable<string>> GetCharacterFriendsNames(int characterid)
        {
            IEnumerable<string> friends;
            var parameters = new DynamicParameters();

            parameters.Add("CharacterId", characterid, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"SELECT Name FROM Character WHERE Character.CharacterId IN ( (SELECT Friend_of FROM Friendship WHERE Friend_to = @Characterid)  UNION  (SELECT Friend_to FROM Friendship WHERE Friend_of = @Characterid))";
                friends = await conn.QueryAsync<string>(query, parameters, commandType: CommandType.Text);
            }
            return friends;
        }

        ////// EPISODES

        public async Task<IEnumerable<Episode>> GetCharacterEpisodesAppearance(int characterid)
        {
            IEnumerable<Episode> episodes;
            var parameters = new DynamicParameters();

            parameters.Add("CharacterId", characterid, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"SELECT Episode.EpisodeId, Episode.Name FROM Appearance INNER JOIN Character ON Appearance.CharacterId = Character.CharacterId INNER JOIN Episode ON Appearance.EpisodeId = Episode.EpisodeId WHERE  (Character.CharacterId = @Characterid) ";
                episodes = await conn.QueryAsync<Episode>(query, parameters, commandType: CommandType.Text);
            }
            return episodes;
        }

        public async Task<IEnumerable<Episode>> GetAllEpisodes()
        {
            IEnumerable<Episode> episodes;
            
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"SELECT        EpisodeId, Name FROM            Episode";
                episodes = await conn.QueryAsync<Episode>(query, commandType: CommandType.Text);
            }
            return episodes;
        }

    }
}
