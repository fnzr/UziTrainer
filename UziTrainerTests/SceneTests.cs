using System;
using UziTrainer.Scenes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UziTrainer;

namespace UziTrainerTests
{
    [TestClass]
    public class SceneTests
    {
        [TestInitialize]
        public void Init()
        {
            Window.Init();
        }

        [TestMethod]
        public void TestDollReplace()
        {
            Formation formation = new Formation();
            var doll1 = Doll.Get("G11");
            var doll2 = Doll.Get("M4 SOP MODII");
            formation.ReplaceDoll(doll1, doll2);
        }
    }
}
