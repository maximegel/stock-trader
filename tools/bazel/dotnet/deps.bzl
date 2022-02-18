".NET rules dependencies"

load(
    "@io_bazel_rules_dotnet//dotnet:defs.bzl",
    "dotnet_register_toolchains",
    "dotnet_repositories_nugets",
)

def dotnet_dependencies():
    dotnet_register_toolchains()
    dotnet_repositories_nugets()
