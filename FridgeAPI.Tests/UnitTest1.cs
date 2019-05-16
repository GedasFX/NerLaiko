using System;
using Xunit;
using Xunit.Abstractions;
using FridgeAPI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FridgeAPI.Tests
{
    public class FridgeTests
    {
        [Fact]
        public void FridgeLogMakesSense()
        {
            IFridge f = new Fridge();

            Dictionary<string, int> contents = new Dictionary<string, int>();

            foreach(KeyValuePair<string, int> log in f.GetActivityLog(new Guid()))
            {
                Assert.True(contents.GetValueOrDefault(log.Key, 0) + log.Value >= 0);

                if (!contents.ContainsKey(log.Key))
                    contents.Add(log.Key, 0);

                contents[log.Key] += log.Value;
            }
        }

        [Fact]
        public void FridgesWithSameGuidProduceSameLogs()
        {
            Guid guid = new Guid();
            IFridge f = new Fridge();

            var list1 = f.GetActivityLog(guid);
            var list2 = f.GetActivityLog(guid);

            Assert.Empty(list1.Except(list2));
            Assert.Empty(list2.Except(list1));
        }

        [Fact]
        public void FridgesWithDifferentGuidProduceDifferentLogs()
        {
            Guid guid = new Guid("398d5c11-9817-408a-b6cd-041a48ff0330");
            Guid guid2 = new Guid("e4c9147b-5a7e-4bee-b6d6-b74219dd5732");
            IFridge f = new Fridge();

            var list1 = f.GetActivityLog(guid);
            var list2 = f.GetActivityLog(guid2);

            Assert.NotEqual(guid, guid2);
            Assert.True(list1.Except(list2).Count() > 0 || list2.Except(list1).Count() > 0);
        }
    }
}
