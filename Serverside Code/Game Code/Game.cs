﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using PlayerIO.GameLibrary;
using System.Drawing;

namespace OneLineDefense {
    public class Player : BasePlayer {
    }

    [RoomType ("TestingRoom")]
    public class GameCode : Game<Player> {
        #region Base methods
        public override void GameStarted () {
            // anything you write to the Console will show up in the 
            // output window of the development server
            Console.WriteLine ("Game is started: " + RoomId);
        }

        // This method is called when the last player leaves the room, and it's closed down.
        public override void GameClosed () {
            Console.WriteLine ("RoomId: " + RoomId);
        }

        // This method is called whenever a player joins the game
        public override void UserJoined (Player player) {
            foreach (Player pl in Players) {
                if (pl.ConnectUserId != player.ConnectUserId) {
                    pl.Send ("PlayerJoined", player.ConnectUserId, 0, 0);
                    player.Send ("PlayerJoined", pl.ConnectUserId);
                }
            }
        }

        // This method is called when a player leaves the game
        public override void UserLeft (Player player) {
            Broadcast ("PlayerLeft", player.ConnectUserId);
        }

        // This method is called when a player sends a message into the server code
        public override void GotMessage (Player player, Message message) {
            Console.WriteLine ("Message from client: " + player.ConnectUserId + ": " + message.Type);
            switch (message.Type) {
                case "Chat":
                    foreach (Player pl in Players) {
                        if (pl.ConnectUserId != player.ConnectUserId) {
                            pl.Send ("Chat", player.ConnectUserId, message.GetString (0));
                        }
                    }
                    break;
            }
        }
        #endregion
    }
}