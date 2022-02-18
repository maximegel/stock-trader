"Bazel functions for installing .NET rules."

load("@bazel_tools//tools/build_defs/repo:git.bzl", "git_repository")

def dotnet_rules_install():
    git_repository(
        name = "io_bazel_rules_dotnet",
        remote = "https://github.com/bazelbuild/rules_dotnet",
        # The 'commit' and 'shallow_since' fields should be used instead of
        # 'branch' to achieve canonical reproducibility.
        commit = "7679ec59712b3e8242cf6be1aeeeca3d80b95408",
        shallow_since = "1643223756 +0000",
        # branch = "master",
    )
