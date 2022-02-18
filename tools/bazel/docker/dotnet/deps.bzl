"Go containers dependencies"

load("@io_bazel_rules_docker//container:container.bzl", "container_pull")

def docker_dotnet_dependencies():
    container_pull(
        name = "aspnet_base",
        registry = "mcr.microsoft.com",
        repository = "dotnet/aspnet",
        # The 'digest' field should be used instead of 'tag' to achieve
        # canonical reproducibility.
        digest = "sha256:b558c4ead392ea04f31ed1a32512668f02e567431d9da7caba74ad75c7c10e38",
        # tag = "6.0-alpine",
    )
