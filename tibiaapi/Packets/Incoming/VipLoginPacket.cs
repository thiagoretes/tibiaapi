﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class VipLoginPacket : IncomingPacket
    {

        public uint PlayerId { get; set; }

        public VipLoginPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.VIP_LOGIN;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.VIP_LOGIN)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.VIP_LOGIN;

            PlayerId = msg.GetUInt32();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddUInt32(PlayerId);

            return msg.Packet;
        }
    }
}