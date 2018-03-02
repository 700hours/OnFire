using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OnFire.Projectiles
{
	public class Fire : ModProjectile
	{
		private static int time = 0, randSlow, randFast;
		private const float gravity = 0.0612f;	
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fire");
		}
		public override void SetDefaults()
		{
			projectile.width = 1;
			projectile.height = 1;
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
		public override void AI()
		{
			time++;
			Projectile P = projectile;
			P.velocity.Y += gravity;
			float r = 16f;
			int rand = Main.rand.Next(1, 3);
			float directionX = (float)Math.Cos(P.velocity.X);
			float directionY = (float)Math.Sin(P.velocity.Y);
			Color newColor = default(Color);
			Vector2 tilev = new Vector2(P.position.X/16, P.position.Y/16);
			if((Main.tile[(int)tilev.X, (int)tilev.Y].wall == 4 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 27 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 41 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 42 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 43 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 44 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 60 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 78 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 85 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 106 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 114 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 138 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 139 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 140 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 141 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 149 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 150 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 151 || Main.tile[(int)tilev.X, (int)tilev.Y].wall == 152) && Main.rand.Next(30) == 0){
				WorldGen.KillWall((int)tilev.X, (int)tilev.Y);
				if(Main.netMode != 0) NetMessage.SendTileSquare(Main.myPlayer, (int)tilev.X-1, (int)tilev.Y-1, 3);
				if(Main.rand.Next(10) == 0){
					for(int i = 1; i < 3; i++){
						int random = Main.rand.Next(-4,1);
						Projectile.NewProjectile(P.position.X, P.position.Y, random, random, mod.ProjectileType("Fire"), 0, 0f, Main.myPlayer);
					}
				}
			}
			if(Main.tile[(int)tilev.X, (int)tilev.Y].active()){
				P.velocity = new Vector2(0, 0);
				if(Main.tile[(int)tilev.X, (int)tilev.Y].type == 5 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 10 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 11 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 14 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 15 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 18 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 19 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 20 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 30 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 50 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 55 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 79 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 86 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 87 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 88 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 89 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 91 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 94 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 101 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 104 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 106 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 114 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 128){
					if(time%4 == 0){
						int a = Dust.NewDust(new Vector2(P.position.X - r/2, P.position.Y - r/2), (int)r, (int)r, 6, 0f, 0f, 0, newColor, 2f);
						Main.dust[a].noGravity = true;
						Main.dust[a].velocity *= 1.4f;
						Main.dust[a].velocity.Y -= 2f;
					}
					if(time%31 == 0){
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/onfire"), P.Center);
					}
				}
				else{
					for(int i = 1; i < 3; i++){
						int b = Dust.NewDust(new Vector2(P.position.X - r/2, P.position.Y - r/2), (int)r, (int)r, 1, 0f, 0f, 0, newColor, 1f);
						Main.dust[b].noGravity = true;
						Main.dust[b].velocity *= 1.4f;
					}
					P.active = false;
				}
			}
			if(Main.tile[(int)tilev.X, (int)tilev.Y].liquid > 20){
			//	int rand = Main.rand.Next(0, 2);
			//	if(rand == 0) Main.PlaySound(2, (int)P.position.X, (int)P.position.Y,SoundHandler.soundID["sizzle"]);
			//	else if(rand == 1) Main.PlaySound(2, (int)P.position.X, (int)P.position.Y,SoundHandler.soundID["sizzle2"]);
			//	else if(rand == 2) Main.PlaySound(2, (int)P.position.X, (int)P.position.Y,SoundHandler.soundID["sizzle3"]);
				P.active = false;
			}
			if(time%150 == 0 && Main.rand.Next(10) == 0){
				for(int i = 1; i < 3; i++){
					int random = Main.rand.Next(-4,1);
					Projectile.NewProjectile(P.position.X, P.position.Y, random, random, mod.ProjectileType("Fire"), 0, 0f, Main.myPlayer);
				}
			}
			if(P.velocity != new Vector2(0, 0)){
				if(time%3 == 0){
					int c = Dust.NewDust(new Vector2(P.position.X, P.position.Y), 1, 1, 6, 0f, 0f, 0, newColor, 1.5f);
					Main.dust[c].noGravity = true;
				}
			}
			tilev = new Vector2(P.position.X/16, (P.position.Y/16)-1);
			if(Main.rand.Next(180) == 0){
				if(Main.tile[(int)tilev.X, (int)tilev.Y].active() && (Main.tile[(int)tilev.X, (int)tilev.Y].type == 5 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 10 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 11 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 14 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 15 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 18 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 19 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 20 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 30 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 50 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 55 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 79 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 86 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 87 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 88 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 89 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 91 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 94 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 101 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 104 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 106 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 114 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 128 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 157 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 158 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 159 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 208 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 252 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 253 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 304 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 311 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 321 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 322 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 323 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 383 || Main.tile[(int)tilev.X, (int)tilev.Y].type == 384)){
					Projectile.NewProjectile(P.position.X, P.position.Y - 16f, 0f, 0f, mod.ProjectileType("Fire"), 0, 0f, Main.myPlayer);
				}
			}
			Rectangle prjB = new Rectangle((int)P.position.X, (int)P.position.Y, P.width, P.height);
			foreach(Player player in Main.player){
				if(!player.active) continue;
				if(player.dead) continue;
				if(player.statLife <= 0) continue;
				Rectangle plrB = new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height);
				if(prjB.Intersects(plrB)){
					player.AddBuff(24, 300, false);
					if(Main.netMode != 0) NetMessage.SendData(50, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			foreach(NPC N in Main.npc){
				if(!N.active) continue;
				if(N.life <= 0) continue;
				if(N.friendly) continue;
				if(N.dontTakeDamage) continue;
				Rectangle nB = new Rectangle((int)N.position.X, (int)N.position.Y, N.width, N.height);
				if(prjB.Intersects(nB)){
					N.AddBuff(24, 600, false);
					if(Main.netMode != 0) NetMessage.SendData(54, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}
		public override void Kill(int timeLeft)
		{
			Projectile P = projectile;
			Vector2 tilev = new Vector2(P.position.X/16, P.position.Y/16);
			if(P.velocity == new Vector2(0, 0)){
				WorldGen.KillTile((int)tilev.X, (int)tilev.Y, false, false, true);
				//	networking
				if(Main.netMode != 0) {
					NetMessage.SendData(20, -1, -1, null, 0, (float)tilev.X, (float)tilev.Y, 0f, 0, 0, 0);
					NetMessage.SendTileSquare(Main.myPlayer, (int)tilev.X-1, (int)tilev.Y-1, 3);
				}
				if(Main.tile[(int)tilev.X, (int)tilev.Y].wall == 4){
					WorldGen.KillWall((int)tilev.X, (int)tilev.Y);
					if(Main.netMode != 0) {
						NetMessage.SendData(20, -1, -1, null, 0, (float)tilev.X, (float)tilev.Y, 0f, 0, 0, 0);
						NetMessage.SendTileSquare(Main.myPlayer, (int)tilev.X-1, (int)tilev.Y-1, 3);
					}
				}
				if(Main.rand.Next(10) == 0){
					for(int i = 1; i < 3; i++){
						int random = Main.rand.Next(-4,1);
						Projectile.NewProjectile(P.position.X, P.position.Y, random, random, mod.ProjectileType("Fire"), 0, 0f, Main.myPlayer);
					}
				}
			}
		}
	}
}
