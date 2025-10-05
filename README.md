# 🎮 Dominoise - Modern Domino Game

A modern, minimalist domino game built with Unity, featuring camera-based player detection and a sleek Apple-inspired UI design.

## ✨ Features

### 🎯 Core Gameplay
- **Camera-based Player Detection** - Uses front camera to detect players
- **Real-time Game Flow** - Smooth state management
- **Modern UI System** - Apple-inspired minimalist design
- **Cross-platform Support** - iOS and Android ready

### 🎨 UI Design
- **Modern Minimal Theme** - Black, white, and gray color palette
- **Apple-style Design** - Clean, universal interface
- **Smooth Animations** - Subtle, professional transitions
- **Responsive Layout** - Adapts to different screen sizes

### 🔧 Technical Features
- **Service Locator Pattern** - Clean architecture
- **Event-driven System** - Loose coupling between components
- **Camera Management** - Multi-camera support
- **Performance Optimized** - 60+ FPS on mobile devices

## 🚀 Getting Started

### Prerequisites
- Unity 2022.3 LTS or later
- iOS/Android development environment
- Camera permissions for player detection

### Installation
1. Clone the repository:
```bash
git clone https://github.com/yourusername/Dominoise-1.0.git
cd Dominoise-1.0
```

2. Open the project in Unity
3. Configure build settings for your target platform
4. Build and run on device (camera features require real device)

## 🎮 How to Play

1. **Launch the game** - Modern main menu with clean design
2. **Position players** - Front camera detects players automatically
3. **Start playing** - Game flow manages states smoothly
4. **Enjoy** - Minimalist UI keeps focus on gameplay

## 🧪 Testing

### Real Game Tests
The project includes comprehensive test suites:

- **RealGameTestSuite.cs** - Tests camera detection, player detection, game flow
- **PerformanceTestSuite.cs** - Tests performance, memory usage, frame rate
- **Modern UI Tests** - Tests UI responsiveness and animations

### Running Tests
1. Add `RealGameTestSuite` component to a GameObject
2. Press Play in Unity
3. Use keyboard shortcuts:
   - **T** - Run all tests
   - **C** - Test camera detection
   - **P** - Test player detection
   - **G** - Test game flow
   - **U** - Test UI system

## 📁 Project Structure

```
Assets/Scripts/
├── Game.Core/           # Core game logic
├── Game.Services/       # Service layer
├── Game.UI/             # User interface
│   ├── Themes/          # UI themes
│   ├── Debug/           # Test suites
│   └── Prefabs/         # UI prefabs
└── App/                 # Application entry point
```

## 🎨 UI Themes

### Modern Minimal Theme
- **Colors**: Black (#1A1A1A), Gray (#4D4D4D), White (#F2F2F2)
- **Style**: Apple-inspired, minimalist
- **Animations**: Subtle, smooth transitions
- **Typography**: Clean, readable fonts

## 🔧 Architecture

### Service Locator Pattern
```csharp
// Register services
Service.Register<IEventBus>(new EventBus());
Service.Register<IEconomy>(economyManager);

// Use services
var eventBus = Service.Get<IEventBus>();
```

### Event-driven System
```csharp
// Subscribe to events
eventBus.Subscribe<UiEvents.CountdownShow>(OnCountdownShow);

// Publish events
eventBus.Publish(new UiEvents.CountdownShow(3));
```

## 📱 Platform Support

- **iOS** - Native camera integration
- **Android** - Native camera integration
- **Unity Editor** - Development and testing

## 🚀 Performance

- **60+ FPS** on modern mobile devices
- **Optimized memory usage** with efficient object pooling
- **Smooth animations** with hardware acceleration
- **Fast camera switching** with minimal latency

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🎯 Roadmap

- [ ] Multiplayer support
- [ ] Advanced camera effects
- [ ] Custom themes
- [ ] Analytics integration
- [ ] Cloud save functionality

## 📞 Support

For support and questions:
- Create an issue on GitHub
- Check the documentation
- Review the test suites for examples

---

**Built with ❤️ using Unity and modern C# practices**
