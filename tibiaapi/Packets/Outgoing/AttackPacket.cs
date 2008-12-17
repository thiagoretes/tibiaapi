﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class AttackPacket : OutgoingPacket
    {

        public uint CreatureId { get; set; }

        public AttackPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType_t.ATTACK;
            Destination = PacketDestination_t.SERVER;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType_t.ATTACK)
                return false;

            Destination = destination;
            Type = OutgoingPacketType_t.ATTACK;

            CreatureId = msg.GetUInt32();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddUInt32(CreatureId);

            return msg.Packet;
        }

        public static bool Send(Objects.Client client, uint creatureId)
        {
            AttackPacket p = new AttackPacket(client);
            p.CreatureId = creatureId;
            return p.Send();
        }
    }
}