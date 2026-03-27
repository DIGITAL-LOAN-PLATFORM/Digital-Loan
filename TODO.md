# Startup Crash Fix - Steps (1/8 complete)

## Phase 1: Core Fixes
- [x] Step 1: Create CustomUserClaimsPrincipalFactory.cs ✓
- [x] Step 2: Fix ServiceContainer.cs (clean DI + Cookie auth) ✓
- [x] Step 3: Remove Authentication.cs ✓
- [x] Step 4: Update Program.cs (no sync migrate, dev errors, static files, logging) ✓

## Phase 2: DB & Config
- [x] Step 5: Create async Migration HostedService ✓
- [x] Step 5.1: Register + using in Program.cs ✓
- [x] Step 6: LocalDB fallback in appsettings ✓

## Phase 3: Build/Test
- [x] Step 7: csproj OK (no changes needed) ✓
- [ ] Step 8: Running dotnet watch to verify startup
