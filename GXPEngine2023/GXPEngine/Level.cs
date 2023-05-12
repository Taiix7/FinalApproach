using TiledMapParser;

namespace GXPEngine
{
    class Level : GameObject
    {
        TiledLoader loader;

        public Player player { get; private set; }
        public Level(string filename)
        {
            loader = new TiledLoader(filename);
            CreateLevel();
        }
        void CreateLevel()
        {
            loader.rootObject = this;
            loader.autoInstance = true;
            loader.addColliders = false;
            loader.LoadTileLayers();
            loader.LoadObjectGroups();
            player = FindObjectOfType<Player>();


        }
    }
}
