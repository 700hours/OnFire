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
	public class GProjectile : GlobalProjectile
	{
		private static int random;
	//	private static int buffer;
		public override void Kill(Projectile P, int timeleft)
		{
			Projectile projectile = P;
		//	buffer++;
		//	if(buffer >= 600){ buffer = 0;	}
		//	if(buffer >= 40){
		//	if(Main.rand.Next(15) == 0){
			if(P.type == 2 || P.type == 14 || P.type == 15 || P.type == 28 || P.type == 34 || P.type == 37 || P.type == 41 || P.type == 82 || P.type == 85 || P.type == 100 || P.type == 110 || P.type == 137 || P.type == 140 || P.type == 143 || P.type == 167 || P.type == 168 || P.type == 169 || P.type == 170 || P.type == 187 || P.type == 188 || P.type == 258 || P.type == 259 || P.type == 325 || P.type == 326 || P.type == 327 || P.type == 328 || P.type == 329 || P.type == 376 || P.type == 399 || P.type == 400 || P.type == 401 || P.type == 402 || P.type == 467){
				for(int i = 1; i < 10; i++){
					int randomizer = Main.rand.Next(-4,4);
					if(randomizer == 1) randomizer++;
					else if(randomizer == -1) randomizer--;
					else if(randomizer == 0) randomizer += 2;
					random = randomizer;
					Projectile.NewProjectile(P.position.X + (P.width/2), P.position.Y + (P.height/2), random, random, mod.ProjectileType("Fire"), 0, 0f, Main.myPlayer);
				}
				if(Main.rand.Next(8) == 0)
				{
					for(int i = 1; i < 3; i++){
						int randomizer = Main.rand.Next(-4,4);
						if(randomizer == 1) randomizer++;
						else if(randomizer == -1) randomizer--;
						else if(randomizer == 0) randomizer += 2;
						random = randomizer;
						Projectile.NewProjectile(P.position.X + (P.width/2), P.position.Y + (P.height/2), random, random, mod.ProjectileType("BlueFire"), 0, 0f, Main.myPlayer);
					}
				}
				float Radius = 20f;
				for (int num70 = 0; num70 < 25; num70++)
				{
					int num72 = Dust.NewDust(new Vector2(P.position.X-Radius, P.position.Y-Radius), P.width+(int)Radius, P.height+(int)Radius, 1, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num72].velocity *= 1.4f;
					Main.dust[num72].noGravity = true;
				}
				
			}
		//	}
		//	}
		//	return true;
		}
	}
}
