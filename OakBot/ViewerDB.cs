﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using System.Globalization;

namespace OakBot
{
    public static class ViewerDB
    {
        public static readonly string filename = Config.AppDataPath + "\\OakBotViewers.sqlite";

        /// <summary>
        /// Load viewers from DBfile to colDatabase.
        /// </summary>
        public static void LoadAllViewers()
        {
            SQLiteConnection dbConnection;

            // Create new database-file and table if not exists
            if (!File.Exists(filename))
            {
                SQLiteConnection.CreateFile(filename);

                dbConnection = new SQLiteConnection(string.Format("Data Source={0}; Version=3;", filename));
                dbConnection.Open();

                SQLiteCommand sqlCmd = new SQLiteCommand("CREATE TABLE `Viewers` (`Username` TEXT NOT NULL, `Points` INTEGER, `Spent` INTEGER, `Watched` TEXT, `LastSeen` TEXT, `Raids` INTEGER, `Title` TEXT, `Regular` BOOLEAN, `IGN` TEXT, PRIMARY KEY(Username))", dbConnection);
                sqlCmd.ExecuteNonQuery();

                dbConnection.Close();
            }
            // Load database-file otherwise
            else
            {
                dbConnection = new SQLiteConnection(string.Format("Data Source={0}; Version=3; Read Only=True;", filename));
                dbConnection.Open();

                SQLiteCommand read = new SQLiteCommand("SELECT * FROM `Viewers`", dbConnection);
                SQLiteDataReader reader = read.ExecuteReader();

                while (reader.Read())
                {
                    TwitchViewer loadedViewer = new TwitchViewer((string)reader["Username"]);
                    loadedViewer.Points = (long)reader["Points"];
                    loadedViewer.Watched = TimeSpan.Parse((string)reader["Watched"]);
                    loadedViewer.LastSeen = DateTime.Parse((string)reader["LastSeen"], CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
                    loadedViewer.Raids = (long)reader["Raids"];
                    loadedViewer.Title = (string)reader["Title"];
                    loadedViewer.regular = (bool)reader["Regular"];
                    loadedViewer.IGN = (string)reader["IGN"];

                    MainWindow.colDatabase.Add(loadedViewer);
                }

                dbConnection.Close();
            }
        }



        /// <summary>
        /// Add all viewers to DBfile in colDatabase.
        /// Does not check if viewers already exists, make sure they don't prior calling.
        /// </summary>
        public static void AddAllViewers()
        {
            SQLiteCommand sqlCmd;

            // Open DBfile
            SQLiteConnection dbConnection = new SQLiteConnection(string.Format("Data Source={0}; Version=3", filename));
            dbConnection.Open();

            // Insert new TwitchViewer in `Viewers`
            foreach (TwitchViewer viewer in MainWindow.colDatabase)
            {
                sqlCmd = new SQLiteCommand(
                    string.Format("INSERT INTO `Viewers` VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')",
                        viewer.UserName,
                        viewer.Points,
                        viewer.Spent,
                        viewer.Watched.ToString(),
                        viewer.LastSeen.ToString("o"),
                        viewer.Raids,
                        viewer.Title,
                        viewer.regular.ToString(),
                        viewer.IGN),
                    dbConnection);
                sqlCmd.ExecuteNonQuery();
            }

            // Close DBfile
            dbConnection.Close();
        }

        /// <summary>
        /// Update all viewers in DBfile from colDatabase.
        /// Does not check if viewers exists, make sure they do prior calling.
        /// </summary>
        public static void UpdateAllViewers()
        {
            SQLiteConnection dbConnection = new SQLiteConnection(string.Format("Data Source={0}; Version=3", filename));
            dbConnection.Open();

            SQLiteCommand sqlCmd;

            foreach(TwitchViewer viewer in MainWindow.colDatabase)
            {
                sqlCmd = new SQLiteCommand(
                    string.Format("UPDATE `Viewers` SET `Points` = '{1}', `Spent` = '{2}', `Watched` = '{3}', `LastSeen` = '{4}', `Raids` = '{5}', `Title` = '{6}', `Regular` = '{7}', `IGN` = '{8}' WHERE `Username` = '{0}'",
                        viewer.UserName,
                        viewer.Points,
                        viewer.Spent,
                        viewer.Watched.ToString(),
                        viewer.LastSeen.ToString("o"),
                        viewer.Raids,
                        viewer.Title,
                        viewer.regular.ToString(),
                        viewer.IGN),
                dbConnection);

                sqlCmd.ExecuteNonQuery();
            }

            dbConnection.Close();
        }

        /// <summary>
        /// Remove all viewers from DBfile
        /// </summary>
        public static void RemoveAllViewers()
        {
            SQLiteCommand sqlCmd;

            // Open DBfile
            SQLiteConnection dbConnection = new SQLiteConnection(string.Format("Data Source={0}; Version=3", filename));
            dbConnection.Open();

            // Delete all rows in `Viewers`
            sqlCmd = new SQLiteCommand("DELETE FROM `Viewers`", dbConnection);
            sqlCmd.ExecuteNonQuery();

            // Close DBfile
            dbConnection.Close();
        }



        /// <summary>
        /// Add new viewer to DBfile with specified TwitchViewer.
        /// Does not check if viewer exists, make sure it doesn't prior calling.
        /// </summary>
        /// <param name="viewer">Viewer to be added</param>
        public static void AddViewer(TwitchViewer viewer)
        {
            SQLiteConnection dbConnection = new SQLiteConnection(string.Format("Data Source={0}; Version=3", filename));
            dbConnection.Open();

            SQLiteCommand sqlCmd = new SQLiteCommand(
                string.Format("INSERT INTO `Viewers` VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')",
                    viewer.UserName,
                    viewer.Points,
                    viewer.Spent,
                    viewer.Watched.ToString(),
                    viewer.LastSeen.ToString("o"),
                    viewer.Raids,
                    viewer.Title,
                    viewer.regular.ToString(),
                    viewer.IGN),
                dbConnection);
            sqlCmd.ExecuteNonQuery();

            dbConnection.Close();
        }

        /// <summary>
        /// Update viewer in the DBfile with specified TwitchViewer.
        /// Does not check if viewer exists, make sure it does prior calling.
        /// </summary>
        /// <param name="viewer">Viewer to be updated</param>
        public static void UpdateViewer(TwitchViewer viewer)
        {
            SQLiteConnection dbConnection = new SQLiteConnection(string.Format("Data Source={0}; Version=3", filename));
            dbConnection.Open();

            SQLiteCommand sqlCmd = new SQLiteCommand(
                string.Format("UPDATE `Viewers` SET `Points` = '{1}', `Spent` = '{2}', `Watched` = '{3}', `LastSeen` = '{4}', `Raids` = '{5}', `Title` = '{6}', `Regular` = '{7}', `IGN` = '{8}' WHERE `Username` = '{0}'",
                    viewer.UserName,
                    viewer.Points,
                    viewer.Spent,
                    viewer.Watched.ToString(),
                    viewer.LastSeen.ToString("o"),
                    viewer.Raids,
                    viewer.Title,
                    viewer.regular.ToString(),
                    viewer.IGN),
            dbConnection);
            sqlCmd.ExecuteNonQuery();

            dbConnection.Close();
        }

        /// <summary>
        /// Remove viewer from DBfile specified by TwitchViewer.
        /// Does not check if viewer exists, make sure it does prior calling.
        /// </summary>
        /// <param name="viewer">Viewer to be removed</param>
        public static void RemoveViewer(TwitchViewer viewer)
        {
            SQLiteConnection dbConnection = new SQLiteConnection(string.Format("Data Source={0}; Version=3", filename));
            dbConnection.Open();

            SQLiteCommand sqlCmd = new SQLiteCommand(string.Format("DELETE FROM `Viewers` WHERE `Username` = '{0}'", viewer.UserName), dbConnection);
            sqlCmd.ExecuteNonQuery();
            dbConnection.Close();
        }

    }
}
