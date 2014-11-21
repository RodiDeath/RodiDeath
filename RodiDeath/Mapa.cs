using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace RodiDeath
{
    class Mapa
    {
        public Texture2D textura; //textura del Meco
        public Vector2 pos;

        public Vector2 Posicion
        {
            get { return pos; }
            set { pos = value; }
        }

        public Texture2D Textura
        {
            get { return textura; }
            set { textura = value; }
        }

        public Mapa(int x, int y)
        {
            pos = new Vector2(x, y);
        }
        public void actualiza()
        {

        }

        public void Draw(SpriteBatch batch)
        {
            //Pintamos el Mapa
            batch.Draw(textura, pos, Color.White);

        }

    }
}
