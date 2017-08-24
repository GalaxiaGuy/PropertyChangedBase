﻿using System;
using GamesWithGravitas.Extensions;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GamesWithGravitas
{
    public class NotifyPropertyChangedBaseTests
    {
        [Theory]
        [InlineData("Foo")]
        [InlineData("Bar")]
        public void SettingStringPropertyWorks(string propertyValue)
        {
            var notifier = new Notifier();
            var listener = notifier.ListenForPropertyChanged(nameof(Notifier.StringProperty));

            notifier.StringProperty = propertyValue;

            Assert.Equal(propertyValue, notifier.StringProperty);
            Assert.True(listener.AllTrue);
        }

        [Fact]
        public void SettingObjectPropertyWorks()
        {
            var propertyValue = new object();
            var notifier = new Notifier();
            var listener = notifier.ListenForPropertyChanged(nameof(Notifier.ObjectProperty));

            notifier.ObjectProperty = propertyValue;

            Assert.Equal(propertyValue, notifier.ObjectProperty);
            Assert.True(listener.AllTrue);
        }

        [Fact]
        public void SettingComplexPropertyWorks()
        {
            var propertyValue = new Dictionary<Guid, IObservable<List<KeyValuePair<string, string>>>>();
            var notifier = new Notifier();
            var listener = notifier.ListenForPropertyChanged(nameof(Notifier.ComplexProperty));

            notifier.ComplexProperty = propertyValue;

            Assert.Equal(propertyValue, notifier.ComplexProperty);
            Assert.True(listener.AllTrue);
        }

        [Theory]
        [InlineData(SimpleEnum.Second)]
        [InlineData(SimpleEnum.Third)]
        public void SettingEnumPropertyWorks(SimpleEnum propertyValue)
        {
            var notifier = new Notifier();
            var listener = notifier.ListenForPropertyChanged(nameof(Notifier.EnumProperty));

            notifier.EnumProperty = propertyValue;

            Assert.Equal(propertyValue, notifier.EnumProperty);
            Assert.True(listener.AllTrue);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void SettingIntPropertyWorks(int propertyValue)
        {
            var notifier = new Notifier();
            var listener = notifier.ListenForPropertyChanged(nameof(Notifier.IntProperty));

            notifier.IntProperty = propertyValue;

            Assert.Equal(propertyValue, notifier.IntProperty);
            Assert.True(listener.AllTrue);
        }

        [Theory]
        [InlineData("Foo")]
        [InlineData("Bar")]
        public void SettingDependentPropertyWorks(string propertyValue)
        {
            var notifier = new Notifier();
            var listener = notifier.ListenForPropertyChanged(nameof(Notifier.DependentProperty), nameof(Notifier.DerivedProperty));

            notifier.DependentProperty = propertyValue;

            Assert.Equal(propertyValue, notifier.DependentProperty);
            Assert.Equal(propertyValue + "!", notifier.DerivedProperty);
            Assert.True(listener.AllTrue);
        }
    }

    public class Notifier : NotifyPropertyChangedBase
    {
        private string _stringProperty;
        public string StringProperty
        {
            get => _stringProperty;
            set => SetProperty(ref _stringProperty, value);
        }

        private object _objectProperty;
        public object ObjectProperty
        {
            get => _objectProperty;
            set => SetProperty(ref _objectProperty, value);
        }

        private Dictionary<Guid, IObservable<List<KeyValuePair<string, string>>>> _complexProperty;
        public Dictionary<Guid, IObservable<List<KeyValuePair<string, string>>>> ComplexProperty
        {
            get => _complexProperty;
            set => SetProperty(ref _complexProperty, value);
        }

        private SimpleEnum _enumProperty;
        public SimpleEnum EnumProperty
        {
            get => _enumProperty;
            set => SetEnumProperty(ref _enumProperty, value);
        }

        private int _intProperty;
        public int IntProperty
        {
            get => _intProperty;
            set => SetProperty(ref _intProperty, value);
        }

        private string _dependentProperty;
        public string DependentProperty
        {
            get => _dependentProperty;
            set => SetProperty(ref _dependentProperty, value, otherProperties: nameof(DerivedProperty));
        }

        public string DerivedProperty => DependentProperty + "!";
    }

    public enum SimpleEnum
    {
        First,
        Second,
        Third
    }
}
