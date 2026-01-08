#!/bin/bash

echo "========================================"
echo "Verificar Versoes do .NET e dotnet-ef"
echo "========================================"

# 1. Versao do .NET SDK
echo "[1/5] Versao do .NET SDK instalada:"
echo "----------------------------------------"
dotnet --version
echo ""

# 2. Versoes do .NET Runtime instaladas
echo "[2/5] Versoes do .NET Runtime instaladas:"
echo "----------------------------------------"
dotnet --list-runtimes
echo ""

# 3. Versao do dotnet-ef
echo "[3/5] Versao do dotnet-ef instalada:"
echo "----------------------------------------"
dotnet ef --version 2>&1
echo ""

# 4. Versoes dos pacotes no projeto
echo "[4/5] Verificando versoes dos pacotes no SGSB.Web.csproj:"
echo "----------------------------------------"
if [ -f "SGSB.Web/SGSB.Web.csproj" ]; then
    echo "Microsoft.EntityFrameworkCore.SqlServer:"
    grep "Microsoft.EntityFrameworkCore.SqlServer" SGSB.Web/SGSB.Web.csproj | grep -oP 'Version="\K[^"]+'
    echo ""
    echo "Microsoft.Data.SqlClient ou System.Data.SqlClient:"
    grep -E "Microsoft.Data.SqlClient|System.Data.SqlClient" SGSB.Web/SGSB.Web.csproj | grep -oP 'Version="\K[^"]+' || echo "Nao encontrado"
    echo ""
    echo "TargetFramework:"
    grep "TargetFramework" SGSB.Web/SGSB.Web.csproj | head -1
else
    echo "Arquivo SGSB.Web.csproj nao encontrado"
fi
echo ""

# 5. Versoes dos pacotes restaurados
echo "[5/5] Verificando versoes dos pacotes restaurados:"
echo "----------------------------------------"
if [ -f "SGSB.Web/obj/project.assets.json" ]; then
    echo "Microsoft.EntityFrameworkCore.SqlServer:"
    grep -A 5 '"Microsoft.EntityFrameworkCore.SqlServer"' SGSB.Web/obj/project.assets.json | grep '"version"' | head -1
    echo ""
    echo "Microsoft.Data.SqlClient:"
    grep -A 5 '"Microsoft.Data.SqlClient"' SGSB.Web/obj/project.assets.json | grep '"version"' | head -1 || echo "Nao encontrado"
    echo ""
    echo "System.Data.SqlClient:"
    grep -A 5 '"System.Data.SqlClient"' SGSB.Web/obj/project.assets.json | grep '"version"' | head -1 || echo "Nao encontrado"
else
    echo "Arquivo project.assets.json nao encontrado. Execute 'dotnet restore' primeiro."
fi
echo ""

echo "========================================"
echo "Comparacao:"
echo "========================================"
echo "No Visual Studio (Windows), verifique:"
echo "  - dotnet --version"
echo "  - dotnet ef --version"
echo "  - Versoes dos pacotes NuGet no projeto"
echo ""
echo "Se as versoes forem diferentes, isso pode explicar"
echo "por que funciona offline mas nao online!"
echo ""

