using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using PlayerIO.GameLibrary;
using System.Drawing;

namespace OneLineDefense {
    public class Player : BasePlayer {
    }

    [RoomType("TestingRoom")]
    public class GameCode : Game<Player> {
        #region Base methods
        public override void GameStarted() {
            // anything you write to the Console will show up in the 
            // output window of the development server
            Console.WriteLine("Game is started: " + RoomId);
        }

        // This method is called when the last player leaves the room, and it's closed down.
        public override void GameClosed() {
            Console.WriteLine("RoomId: " + RoomId);
        }

        // This method is called whenever a player joins the game
        public override void UserJoined(Player player) {
            foreach (Player pl in Players) {
                if (pl.ConnectUserId != player.ConnectUserId) {
                    pl.Send("PlayerJoined", player.ConnectUserId, 0, 0);
                    player.Send("PlayerJoined", pl.ConnectUserId);
                }
            }
        }

        // This method is called when a player leaves the game
        public override void UserLeft(Player player) {
            Broadcast("PlayerLeft", player.ConnectUserId);
        }

        // This method is called when a player sends a message into the server code
        public override void GotMessage(Player player, Message message) {
            Console.WriteLine("Message from client: " + player.ConnectUserId + ": " + message.Type);
            switch (message.Type) {
                case "Chat":
                    this.Chat(player, message);
                    break;
                case "CreateUser":
                    this.CreateUser(player, message);
                    break;
                case "UpdateUser":
                    this.UpdateUser(player, message);
                    break;
            }
        }
        #endregion

        #region "GotMessage" methods
        private void Chat(Player player, Message message) {
            foreach (Player pl in Players) {
                if (pl.ConnectUserId != player.ConnectUserId) {
                    pl.Send("Chat", player.ConnectUserId, message.GetString(0));
                }
            }
        }

        private void CreateUser(Player player, Message message) {
            DatabaseObject user = new DatabaseObject();
            user.Set("name", message.GetString(0));
            PlayerIO.BigDB.CreateObject("Users", null, user,
                delegate(DatabaseObject dbo) {
                    // Success
                    Console.WriteLine("User " + user.GetString("name") + " created");
                },
                delegate(PlayerIOError error) {
                    // Error
                    Console.WriteLine(error.Message);
                });
        }

        private void UpdateUser(Player player, Message message) {
            PlayerIO.BigDB.LoadSingle("Users", "byName",
                new object[] { message.GetString(0) },
                delegate(DatabaseObject userToUpdate) {
                    userToUpdate.Set("test", message.GetString(1));
                    userToUpdate.Save(delegate() {
                        player.Send("UserUpdated", string.Format(
                            "L'attribut test du joueur \"{0}\" à été changée par: {1}",
                            message.GetString(0), message.GetString(1)));

                    });
                },
                delegate(PlayerIOError error) {
                    Console.WriteLine(error.Message);
                });
        }

        #endregion
    }
}
