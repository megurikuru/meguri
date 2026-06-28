using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Meguri.Areas.Identity {
    public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber {
        private readonly IStringLocalizer _localizer;

        // IStringLocalizerFactoryを使って、特定の名前（IdentityErrors）のリソースファイルを読み込む
        public LocalizedIdentityErrorDescriber(IStringLocalizerFactory factory) {
            // フォルダ名「Resources」の中にある「SharedResource」リソースを参照する
            _localizer = factory.Create("SharedResource", typeof(LocalizedIdentityErrorDescriber).Assembly.GetName().Name);
        }

        // 英数字以外の文字（記号）が必要なときのエラー
        public override IdentityError PasswordRequiresNonAlphanumeric() {
            return new IdentityError {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = _localizer["PasswordRequiresNonAlphanumeric"]
            };
        }

        // 大文字が必要なときのエラー
        public override IdentityError PasswordRequiresUpper() {
            return new IdentityError {
                Code = nameof(PasswordRequiresUpper),
                Description = _localizer["PasswordRequiresUpper"]
            };
        }

        // --- 以下、不足していたエラー定義を追加 ---

        public override IdentityError PasswordMismatch() {
            return new IdentityError {
                Code = nameof(PasswordMismatch),
                Description = _localizer["PasswordMismatch"]
            };
        }

        public override IdentityError InvalidToken() {
            return new IdentityError {
                Code = nameof(InvalidToken),
                Description = _localizer["InvalidToken"]
            };
        }

        public override IdentityError InvalidUserName(string? userName) {
            return new IdentityError {
                Code = nameof(InvalidUserName),
                Description = string.Format(_localizer["InvalidUserName"], userName)
            };
        }

        public override IdentityError InvalidEmail(string? email) {
            return new IdentityError {
                Code = nameof(InvalidEmail),
                Description = string.Format(_localizer["InvalidEmail"], email)
            };
        }

        public override IdentityError DuplicateUserName(string userName) {
            return new IdentityError {
                Code = nameof(DuplicateUserName),
                Description = string.Format(_localizer["DuplicateUserName"], userName)
            };
        }

        public override IdentityError DuplicateEmail(string email) {
            return new IdentityError {
                Code = nameof(DuplicateEmail),
                Description = string.Format(_localizer["DuplicateEmail"], email)
            };
        }

        public override IdentityError InvalidRoleName(string? roleName) {
            return new IdentityError {
                Code = nameof(InvalidRoleName),
                Description = string.Format(_localizer["InvalidRoleName"], roleName)
            };
        }

        public override IdentityError DuplicateRoleName(string roleName) {
            return new IdentityError {
                Code = nameof(DuplicateRoleName),
                Description = string.Format(_localizer["DuplicateRoleName"], roleName)
            };
        }

        public override IdentityError UserAlreadyHasPassword() {
            return new IdentityError {
                Code = nameof(UserAlreadyHasPassword),
                Description = _localizer["UserAlreadyHasPassword"]
            };
        }

        public override IdentityError UserAlreadyInRole(string role) {
            return new IdentityError {
                Code = nameof(UserAlreadyInRole),
                Description = string.Format(_localizer["UserAlreadyInRole"], role)
            };
        }

        public override IdentityError UserNotInRole(string role) {
            return new IdentityError {
                Code = nameof(UserNotInRole),
                Description = string.Format(_localizer["UserNotInRole"], role)
            };
        }

        public override IdentityError PasswordTooShort(int length) {
            return new IdentityError {
                Code = nameof(PasswordTooShort),
                Description = string.Format(_localizer["PasswordTooShort"], length)
            };
        }

        public override IdentityError PasswordRequiresDigit() {
            return new IdentityError {
                Code = nameof(PasswordRequiresDigit),
                Description = _localizer["PasswordRequiresDigit"]
            };
        }

        public override IdentityError PasswordRequiresLower() {
            return new IdentityError {
                Code = nameof(PasswordRequiresLower),
                Description = _localizer["PasswordRequiresLower"]
            };
        }

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) {
            return new IdentityError {
                Code = nameof(PasswordRequiresUniqueChars),
                Description = string.Format(_localizer["PasswordRequiresUniqueChars"], uniqueChars)
            };
        }

        public override IdentityError RecoveryCodeRedemptionFailed() {
            return new IdentityError {
                Code = nameof(RecoveryCodeRedemptionFailed),
                Description = _localizer["RecoveryCodeRedemptionFailed"]
            };
        }
    }
}