using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OnFire.Projectiles
{
	public class FireParticle : ModProjectile
	{
		private static int time = 0, randSlow, randFast;
		private const float gravity = 0.0612f;	
		public static bool 
			noGravity = true, 
			noDamage = true, 
			light = true, 
			init = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fire");
		}
		public override void SetDefaults()
		{
			projectile.width = 3;
			projectile.height = 3;
			projectile.aiStyle = 0;
			projectile.timeLeft = 300;
			projectile.friendly = true;
			projectile.hostile = true;
			projectile.penetrate = 1;
			projectile.tileCollide = false;
			projectile.ignoreWater = false;
			projectile.scale = 1f;
			projectile.ownerHitCheck = true;
			projectile.magic = true;
			projectile.alpha = 255;
			projectile.netUpdate = true;
		}
		public void CustomInit()
		{
			projectile.scale = (float)Main.rand.Next(1,3)/3f;
			projectile.scale += (float)Main.rand.Next(0,2)/2.25f;
			projectile.velocity += new Vector2((float)Main.rand.Next(-2,2)/1.33f, (float)Main.rand.Next(-2,2)/1.33f);
		}
		public override void AI()
		{
			if(!init)
			{
				CustomInit();
				init = true;
			}
			Projectile P = projectile;
			float r = (float)Math.PI/10;
			P.rotation += r;
			P.scale -= 0.025f;
			if(P.scale <= 0) P.active = false;
			else P.active = true;
			if(P.timeLeft%60 == 0) projectile.velocity += new Vector2((float)Main.rand.Next(-2,2)*0.33f, (float)Main.rand.Next(-2,2)*0.33f);
			if(!noGravity) P.velocity.Y += World.gravity;
			if(light) Lighting.AddLight((int)P.position.X/16, (int)P.position.Y/16,(float)160/255, (float)50/255, 0f);
			if(!noDamage)
			{
				Rectangle prjB = new Rectangle((int)P.position.X, (int)P.position.Y, P.width, P.height);
				foreach(Player player in Main.player)
				{
					if(!player.active) continue;
					if(player.dead) continue;
					if(player.statLife <= 0) continue;
					Rectangle plrB = new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height);
					if(prjB.Intersects(plrB))
					{
						player.AddBuff(24, 300, false);
						if(Main.netMode != 0) NetMessage.SendData(50, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				foreach(NPC N in Main.npc)
				{
					if(!N.active) continue;
					if(N.life <= 0) continue;
					if(N.friendly) continue;
					if(N.dontTakeDamage) continue;
					Rectangle nB = new Rectangle((int)N.position.X, (int)N.position.Y, N.width, N.height);
					if(prjB.Intersects(nB))
					{
						N.AddBuff(24, 600, false);
						if(Main.netMode != 0) NetMessage.SendData(54, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}					
			}
		}
	}
}