{ pkgs, lib, config, inputs, ... }:

{
  # https://devenv.sh/basics/
  # env.GREET = "devenv";

  # https://devenv.sh/packages/
  packages = with pkgs;
    [
      podman
      podman-compose
      pods
      git
      csharp-ls
      (dotnetCorePackages.combinePackages [
      dotnet-sdk_10
      dotnet-sdk_9
      ])
    ];

  # https://devenv.sh/languages/
  languages = {
    # dotnet = {
    #   enable = true;
    #   package = pkgs.dotnet-sdk_9;
    #   lsp.enable = true;
    # };
    nix ={
      enable = true;
      lsp.enable = true;
    };
  };

  # https://devenv.sh/processes/
  # processes.dev.exec = "${lib.getExe pkgs.watchexec} -n -- ls -la";

  # https://devenv.sh/services/
  # services.postgres.enable = true;

  # tenho um problema com o ip6
  env = {
    DOTNET_SYSTEM_NET_DISABLEIPV6 = "1";
  };

  # https://devenv.sh/scripts/
  # scripts.hello.exec = ''
  #   echo hello from $GREET
  # '';

  # https://devenv.sh/basics/
  # enterShell = ''
  #   # hello         # Run scripts directly
  #   # git --version # Use packages
  #   dotnet --list-sdks
  # '';

  # https://devenv.sh/tasks/
  # tasks = {
  #   "myproj:setup".exec = "mytool build";
  #   "devenv:enterShell".after = [ "myproj:setup" ];
  # };

  # https://devenv.sh/tests/
  # enterTest = ''
  #   echo "Running tests"
  #   git --version | grep --color=auto "${pkgs.git.version}"
  # '';

  # https://devenv.sh/git-hooks/
  # git-hooks.hooks.shellcheck.enable = true;

  # See full reference at https://devenv.sh/reference/options/
}
