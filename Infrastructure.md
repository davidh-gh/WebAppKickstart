# WebAppKickstart

## Signing assembly
To sign assembly, you need to:

Create a key file:
```bash
sn -k MyKey.snk  // constains both public and private keys
sn -p MyKey.snk SigningKey.public.snk // contains only public key
```
*Note: 
- All the dependencies of the assembly must be signed in order to sign the assembly itself.
- The `sn` tool is part of the .NET SDK, so ensure you have it installed.
- You can use `sn -v` to verify the key file.*
- Keep the private key file (`MyKey.snk`) secure and do not commit it to the repository. Add it to `.gitignore` if necessary.
- The public key file (`SigningKey.public.snk`) can be shared or committed to the repository as needed.

#### Add public key to solution files or project files. Add the following to your `Directory.Build.props` file:
```xml
<Project>
  <PropertyGroup>
    <AssemblyOriginatorKeyFile>SigningKey.public.snk</AssemblyOriginatorKeyFile>
    <SignAssembly>true</SignAssembly>
  </PropertyGroup>
</Project>
```

## Pipeline

### Github workflow example

For more info check code_quality.yml github workflow file.
```yaml
# Restore SigningKey from repository secret variable
- name: Restore SigningKey
  run: echo "${{ secrets.STRONG_NAME_KEY_B64 }}" | base64 -d > DhKey.snk

# build and do test steps

# remove the private key file
- name: Clean up Signing Key
  if: always()
  run: rm -f DhKey.snk
```
