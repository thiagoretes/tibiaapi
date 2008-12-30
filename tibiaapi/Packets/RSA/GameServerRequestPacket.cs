﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.RSA
{
    public class GameServerRequestPacket
    {

        public static NetworkMessage CreateGameServerRequestPacket(byte OS, ushort Version,
          string AccountName, string CharacterName, string Password)
        {
            byte[] XteaKey = new byte[16];
            new Random().NextBytes(XteaKey);
            return CreateGameServerRequestPacket(OS, Version, XteaKey, AccountName, CharacterName, Password);
        }

        public static NetworkMessage CreateGameServerRequestPacket(byte OS, ushort Version,
         byte[] XteaKey, string AccountName, string CharacterName, string Password)
        {
            return CreateGameServerRequestPacket(OS, Version, XteaKey, AccountName, CharacterName, Password, false);
        }


        public static NetworkMessage CreateGameServerRequestPacket(byte OS, ushort Version,
         byte[] XteaKey, string AccountName,string CharacterName, string Password, bool OpenTibia)
        {
            NetworkMessage msg = new NetworkMessage(137);
            msg.AddByte(0x95);
            msg.AddByte(0x00);
            msg.Position += 4;
            msg.AddByte(0x0A);
            msg.AddUInt16(OS);
            msg.AddUInt16(Version);
            msg.AddByte(0x0);
            msg.AddBytes(XteaKey);
            msg.AddString(AccountName);
            msg.AddString(CharacterName);
            msg.AddString(Password);
            if (OpenTibia) msg.RsaOTEncrypt(11);
            else msg.RsaCipEncrypt(11);
            msg.InsertAdler32();
            return msg;
        }
    }
}
