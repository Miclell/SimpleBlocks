
<p align="center">
  <img src="../images/logo.png" alt="SimpleBlocks Logo" width="256" />
</p>

# SimpleBlocks
📘 This README is available in: [Russian](../../README.MD)

**A visual environment for learning programming with multi-language support.**

---
Supported languages:
- C#
- C++
- Dart
- Go
- Java
- JavaScript
- Lua
- PHP
- Python
- Rust  
  _(Any language can be added via config files)_

---

## 🚀 Features
- 🧱 Visual code creation using blocks (similar to Scratch/Blockly)
- ▶️ Code execution via Judge0 (C#, Python, etc.)
- 🧩 Flexible architecture (.NET 9 + Blazor Standalone)
- 🔧 Easy language addition via `blocks.json` and `semantics.json`

## 🛠 Technologies
- **Backend**: ASP.NET Core (.NET 9)
- **Frontend**: Blazor Standalone
- **Database**: PostgreSQL
- **Code execution**: Judge0

---

## 🏁 Quick Start
### Option 1: Local launch
#### 1. **Clone the repository**:

```bash
git clone https://github.com/SimpleBlocks.git
cd SimpleBlocks
```

#### 2. **Start Postgres and set environment variables (or modify appsettings.json).**

#### 3. **Configure ops/judge0.conf and run Judge0 (via Docker):**

```bash
cd ops  
docker-compose -f docker-compose.judge0.yml up -d  
```

#### 4. **Install .NET 9**.

[Download SDK](https://dotnet.microsoft.com/en-us/download)

#### 5. **Run the server**:

```bash
cd src/SimpleBlocks.Server
dotnet restore # Restore dependencies
dotnet ef database update # Apply migrations
dotnet run
```

#### 6. **Run the client**:

```bash
cd ../SimpleBlocks.Client
dotnet restore
dotnet run 
```

### Option 2: Run with Docker
#### 1. **Set up the .env configuration file and judge0.conf in ops**
#### 2. **Launch the project using `docker-compose`**
```bash
cd ops  

docker-compose -f docker-compose.postgres.yml -f docker-compose.judge0.yml -f docker-compose.server.yml -f docker-compose.client.yml up -d
```

---

## 📝 [Adding a language](LANGUAGE_GUIDE.md)

---

## 🏗 Architecture

- General view:
  ```mermaid
  flowchart LR
      A[Client: code generation from blocks] -->|Send code| B[Server]
      B -->|Execute| C[Judge0]
      B -->|Read/Write| D[PostgreSQL]
      C -->|Store| D
      C -->|Cache| E[Redis]
  ```
- [More details](ARCHITECTURE.md)

---

## 🖼 Interface

<p align="center">
  <img src="../images/preview.png" alt="SimpleBlocks Interface" width="1024" />
</p>

---

## 📜 License
MIT – see [LICENSE](../../LICENSE).

## 🔗 Links:
1. [Judge0 Documentation](https://ce.judge0.com/)
2. [Blockly Documentation](https://developers.google.com/blockly)
