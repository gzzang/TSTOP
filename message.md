# TSTOP ABP 项目创建与执行记录（完整过程）

## 0. 任务目标
按以下要求创建 ABP 项目并记录完整过程：
- 模板命令：
  - `abp new TSTOP -dbms PostgreSQL --no-social-logins --no-multi-tenancy -u angular --sample-crud-page --connection-string "Host=localhost;Port=5432;Database=tstop;Username=gzzang;******;" postgresql`
- PostgreSQL 参考容器命令（18.4）
- 技术栈：dotnet 10.0.3、`Volo.Abp.Studio.Cli`、docker、postgresql:18.4、redis:8.8
- 说明：CI 在线执行，CD 通过本地 runner 执行

---

## 1. 仓库初始状态检查

### 1.1 查看仓库内容
执行命令：
```bash
ls -laR /tmp/workspace/gzzang/TSTOP
```
结果：仓库初始仅包含：
- `.git/`
- `test`（一个简单文本文件）

### 1.2 基线构建/测试能力检查
执行命令：
```bash
cd /tmp/workspace/gzzang/TSTOP && ls -1a
```
结果：无现成构建脚本、无现成项目结构（尚未创建 ABP 项目）。

---

## 2. 工具链检查与安装

### 2.1 检查 dotnet / docker / abp CLI
执行命令：
```bash
cd /tmp/workspace/gzzang/TSTOP
printf '### dotnet --version\n'; dotnet --version || true
printf '\n### dotnet --list-sdks\n'; dotnet --list-sdks || true
printf '\n### docker --version\n'; docker --version || true
printf '\n### abp --version\n'; abp --version || true
```
关键结果：
- `dotnet --version` -> `10.0.300`
- 已安装多个 SDK（含 10.x）
- `docker --version` -> `Docker version 28.0.4`
- `abp --version` -> `command not found`

### 2.2 安装 ABP CLI
执行命令：
```bash
cd /tmp/workspace/gzzang/TSTOP && dotnet tool install -g Volo.Abp.Studio.Cli
```
结果：
- `Tool 'volo.abp.studio.cli' (version '3.0.3') was successfully installed.`

### 2.3 验证 ABP CLI
执行命令：
```bash
export PATH="$PATH:$HOME/.dotnet/tools"
cd /tmp/workspace/gzzang/TSTOP
abp --version
```
结果：
- `3.0.3`

---

## 3. PostgreSQL 18.4 容器启动与问题处理

### 3.1 首次按旧挂载路径启动（失败）
执行命令：
```bash
POSTGRES_DATA_DIR=/tmp/tstop-postgres-data
mkdir -p "$POSTGRES_DATA_DIR"
(docker rm -f postgres-db >/dev/null 2>&1 || true)
docker run -d --name postgres-db -p 5432:5432 -v "$POSTGRES_DATA_DIR":/var/lib/postgresql/data -e POSTGRES_USER=gzzang -e POSTGRES_PASSWORD=zz123123 postgres:18.4
```
随后查看日志：
```bash
docker logs --tail 50 postgres-db
```
关键报错：
- PostgreSQL 18+ 提示挂载点应使用 `/var/lib/postgresql`，不建议用 `/var/lib/postgresql/data`
- 报错核心为检测到不兼容目录布局，容器未正常完成启动

### 3.2 修复挂载路径并重建容器（成功）
执行命令：
```bash
docker rm -f postgres-db
POSTGRES_DATA_DIR=/tmp/tstop-postgres-data
mkdir -p "$POSTGRES_DATA_DIR"
docker run -d --name postgres-db -p 5432:5432 -v "$POSTGRES_DATA_DIR":/var/lib/postgresql -e POSTGRES_USER=gzzang -e POSTGRES_PASSWORD=zz123123 postgres:18.4
```
验证日志：
```bash
sleep 5
docker logs --tail 50 postgres-db
```
结果：
- 日志出现 `database system is ready to accept connections`
- PostgreSQL 18.4 启动成功

---

## 4. ABP 项目创建过程与错误处理

### 4.1 完全按题目模板命令执行（含尾部 `postgresql`，失败）
执行命令：
```bash
abp new TSTOP -dbms PostgreSQL --no-social-logins --no-multi-tenancy -u angular --sample-crud-page --connection-string "Host=localhost;Port=5432;Database=tstop;Username=gzzang;******;" postgresql
```
关键报错：
- `Option names should start with '-' or '--'.`
- 原因：末尾多余参数 `postgresql` 被当作非法参数

处理方式：
- 去掉尾部多余参数 `postgresql`

### 4.2 去掉尾参后首次执行（触发扩展安装）
执行后输出：
- 首次运行自动下载扩展 `Volo.Abp.Studio.Extensions.StandardSolutionTemplates`
- CLI 提示 `The CLI is restarting to apply the changes...`

处理方式：
- 再次执行相同命令

### 4.3 第二次执行时报输出目录非空（失败）
关键报错：
- `The output folder '/tmp/workspace/gzzang/TSTOP' is not empty. Please give an empty folder.`

原因：
- 当前仓库目录非空（已有 `.git` 与文件）

处理方式：
- 指定空子目录输出：`-o /tmp/workspace/gzzang/TSTOP/TSTOP`

### 4.4 连接字符串参数异常（失败）
过程中出现：
- `System.ArgumentException: Couldn't set ... (Parameter '...;timeout')`

原因：
- 连接字符串中密码段传值异常，导致 Npgsql 解析失败

处理方式：
- 清理输出目录后，使用正确连接字符串重跑

### 4.5 最终成功创建
最终执行命令：
```bash
rm -rf /tmp/workspace/gzzang/TSTOP/TSTOP
export PATH="$PATH:$HOME/.dotnet/tools"
abp new TSTOP -dbms PostgreSQL --no-social-logins --no-multi-tenancy -u angular --sample-crud-page --connection-string "Host=localhost;Port=5432;Database=tstop;Username=gzzang;******;" -o /tmp/workspace/gzzang/TSTOP/TSTOP
```
关键成功输出：
- `Creating your solution...`
- `Running task: Initialize Solution`
- `Your solution has been created.`

项目生成位置：
- `/tmp/workspace/gzzang/TSTOP/TSTOP`

---

## 5. Redis 8.8 启动

执行命令：
```bash
(docker rm -f redis-cache >/dev/null 2>&1 || true)
docker run -d --name redis-cache -p 6379:6379 redis:8.8
```
校验日志：
```bash
docker logs --tail 30 redis-cache
```
关键结果：
- `Ready to accept connections tcp`
- Redis 8.8 启动成功

---

## 6. 生成后构建/测试/检查记录

### 6.1 .NET 构建
先尝试（失败）：
```bash
cd /tmp/workspace/gzzang/TSTOP/TSTOP && dotnet build TSTOP.sln
```
报错：
- `MSBUILD : error MSB1009: Project file does not exist. Switch: TSTOP.sln`

原因：
- 模板生成的是 `TSTOP.slnx` 而不是 `TSTOP.sln`

修复后执行（成功）：
```bash
cd /tmp/workspace/gzzang/TSTOP/TSTOP && dotnet build TSTOP.slnx
```
结果：
- Build succeeded
- 1 个 warning：`CS0162 Unreachable code`（模板代码警告）

### 6.2 .NET 测试
执行命令：
```bash
cd /tmp/workspace/gzzang/TSTOP/TSTOP && dotnet test TSTOP.slnx --no-build
```
结果：
- 整体退出成功
- `TSTOP.EntityFrameworkCore.Tests` 显示 `Passed: 6`
- 其他若干测试程序集提示 `No test is available`（模板默认结构导致，非阻塞）

### 6.3 Angular lint
执行命令：
```bash
cd /tmp/workspace/gzzang/TSTOP/TSTOP/angular && yarn lint
```
结果：失败（模板默认规则冲突）
- `@angular-eslint/component-selector` 报错（footer selector 前缀）
- `@angular-eslint/prefer-inject` 报错（构造函数注入建议）

处理方式：
- 本次仅记录，不改模板默认代码逻辑

### 6.4 Angular build
执行命令：
```bash
cd /tmp/workspace/gzzang/TSTOP/TSTOP/angular && yarn build
```
结果：
- 构建成功，产物目录：`/tmp/workspace/gzzang/TSTOP/TSTOP/angular/dist/TSTOP`
- 有一个样式 budget warning（`home.component.scss` 超预算）

### 6.5 Angular test
执行命令1（失败）：
```bash
cd /tmp/workspace/gzzang/TSTOP/TSTOP/angular && yarn test --watch=false --browsers=ChromeHeadless
```
报错：
- 当前测试 runner 需要额外 browser 适配包（如 `@vitest/browser-playwright`）

执行命令2（失败）：
```bash
cd /tmp/workspace/gzzang/TSTOP/TSTOP/angular && yarn test --watch=false
```
报错：
- `No tests found matching ...`（模板中暂无 spec/test 文件）

处理方式：
- 保留现状，记录为模板初始状态（非本任务阻塞项）

---

## 7. 生成出的关键内容
在 `/tmp/workspace/gzzang/TSTOP/TSTOP` 下已生成完整 ABP 项目结构，包括：
- `src/`（后端应用层、域层、EFCore、Host、DbMigrator 等）
- `angular/`（前端）
- `test/`（测试项目）
- `TSTOP.slnx`、`TSTOP.abpsln`、`TSTOP.abpmdl`

---

## 8. CI/CD 执行说明（按需求）

### 8.1 CI 在线执行
建议 CI 在 GitHub Actions 在线执行以下检查：
1. `.NET restore/build/test`
2. `Angular install/lint/build`
3. 可选：容器可用性探测（PostgreSQL/Redis）

### 8.2 CD 本地 runner 执行
CD 由本地 self-hosted runner 执行，原因：
- 可直接访问本地部署网络、数据库与容器环境
- 可执行本地发布脚本与机密环境变量注入

建议 CD runner 具备：
- Docker
- dotnet 10 SDK
- Node/Yarn
- 到目标部署环境的网络权限

---

## 9. 本次任务中出现的所有主要错误与解决方式（汇总）
1. **`abp` 命令不存在**
   - 原因：CLI 未安装
   - 解决：`dotnet tool install -g Volo.Abp.Studio.Cli`

2. **PostgreSQL 18.4 挂载路径不兼容**
   - 原因：使用 `/var/lib/postgresql/data`
   - 解决：改为挂载 `/var/lib/postgresql`

3. **ABP 命令尾部多余 `postgresql` 参数**
   - 原因：非法裸参数
   - 解决：删除尾部 `postgresql`

4. **首次 ABP 执行后 CLI 重启**
   - 原因：自动安装模板扩展
   - 解决：重跑命令

5. **输出目录非空**
   - 原因：仓库目录非空
   - 解决：加 `-o` 输出到空子目录

6. **连接字符串解析异常**
   - 原因：连接字符串参数传值异常，Npgsql 解析失败
   - 解决：修正连接字符串后重试

7. **误用 `TSTOP.sln` 构建失败**
   - 原因：实际解决方案文件为 `TSTOP.slnx`
   - 解决：改用 `dotnet build TSTOP.slnx`

8. **Angular lint/test 初始失败**
   - 原因：模板默认规则/默认无测试文件
   - 解决：作为初始模板状态记录，后续按项目规范再修正

---

## 10. 最终状态
- ABP 项目已成功创建：`/tmp/workspace/gzzang/TSTOP/TSTOP`
- PostgreSQL 18.4 容器已成功运行
- Redis 8.8 容器已成功运行
- 后端 `.NET` 构建成功，测试命令可执行
- 前端 Angular 可构建，lint/test 存在模板初始问题已记录
- 所有过程、命令、错误与修复已完整记录在本文件
