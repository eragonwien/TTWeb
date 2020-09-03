using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using TTWebCommon.Models.Common.Exceptions;

namespace TTWebCommon.Services
{
    public interface IExceptionService
    {
        void HandleMySqlException(MySqlException mySqlException);
    }
    public class ExceptionService : IExceptionService
    {
        public void HandleMySqlException(MySqlException ex)
        {
            var errorCode = (MySqlErrorCode)ex.Number;
            switch (errorCode)
            {
                case MySqlErrorCode.None:
                    break;
                case MySqlErrorCode.HashCheck:
                    break;
                case MySqlErrorCode.ISAMCheck:
                    break;
                case MySqlErrorCode.No:
                    break;
                case MySqlErrorCode.Yes:
                    break;
                case MySqlErrorCode.CannotCreateFile:
                    break;
                case MySqlErrorCode.CannotCreateTable:
                    break;
                case MySqlErrorCode.CannotCreateDatabase:
                    break;
                case MySqlErrorCode.DatabaseCreateExists:
                    break;
                case MySqlErrorCode.DatabaseDropExists:
                    break;
                case MySqlErrorCode.DatabaseDropDelete:
                    break;
                case MySqlErrorCode.DatabaseDropRemoveDir:
                    break;
                case MySqlErrorCode.CannotDeleteFile:
                    break;
                case MySqlErrorCode.CannotFindSystemRecord:
                    break;
                case MySqlErrorCode.CannotGetStatus:
                    break;
                case MySqlErrorCode.CannotGetWorkingDirectory:
                    break;
                case MySqlErrorCode.CannotLock:
                    break;
                case MySqlErrorCode.CannotOpenFile:
                    break;
                case MySqlErrorCode.FileNotFound:
                    break;
                case MySqlErrorCode.CannotReadDirectory:
                    break;
                case MySqlErrorCode.CannotSetWorkingDirectory:
                    break;
                case MySqlErrorCode.CheckRead:
                    break;
                case MySqlErrorCode.DiskFull:
                    break;
                case MySqlErrorCode.DuplicateKey:
                    break;
                case MySqlErrorCode.ErrorOnClose:
                    break;
                case MySqlErrorCode.ErrorOnRead:
                    break;
                case MySqlErrorCode.ErrorOnRename:
                    break;
                case MySqlErrorCode.ErrorOnWrite:
                    break;
                case MySqlErrorCode.FileUsed:
                    break;
                case MySqlErrorCode.FileSortAborted:
                    break;
                case MySqlErrorCode.FormNotFound:
                    break;
                case MySqlErrorCode.GetErrorNumber:
                    break;
                case MySqlErrorCode.IllegalHA:
                    break;
                case MySqlErrorCode.KeyNotFound:
                    break;
                case MySqlErrorCode.NotFormFile:
                    break;
                case MySqlErrorCode.NotKeyFile:
                    break;
                case MySqlErrorCode.OldKeyFile:
                    break;
                case MySqlErrorCode.OpenAsReadOnly:
                    break;
                case MySqlErrorCode.OutOfMemory:
                    break;
                case MySqlErrorCode.OutOfSortMemory:
                    break;
                case MySqlErrorCode.UnexepectedEOF:
                    break;
                case MySqlErrorCode.ConnectionCountError:
                    break;
                case MySqlErrorCode.OutOfResources:
                    break;
                case MySqlErrorCode.UnableToConnectToHost:
                    break;
                case MySqlErrorCode.HandshakeError:
                    break;
                case MySqlErrorCode.DatabaseAccessDenied:
                    break;
                case MySqlErrorCode.AccessDenied:
                    break;
                case MySqlErrorCode.NoDatabaseSelected:
                    break;
                case MySqlErrorCode.UnknownCommand:
                    break;
                case MySqlErrorCode.ColumnCannotBeNull:
                    break;
                case MySqlErrorCode.UnknownDatabase:
                    break;
                case MySqlErrorCode.TableExists:
                    break;
                case MySqlErrorCode.BadTable:
                    break;
                case MySqlErrorCode.NonUnique:
                    break;
                case MySqlErrorCode.ServerShutdown:
                    break;
                case MySqlErrorCode.BadFieldError:
                    break;
                case MySqlErrorCode.WrongFieldWithGroup:
                    break;
                case MySqlErrorCode.WrongGroupField:
                    break;
                case MySqlErrorCode.WrongSumSelected:
                    break;
                case MySqlErrorCode.WrongValueCount:
                    break;
                case MySqlErrorCode.TooLongIdentifier:
                    break;
                case MySqlErrorCode.DuplicateFieldName:
                    break;
                case MySqlErrorCode.DuplicateKeyName:
                    break;
                case MySqlErrorCode.DuplicateKeyEntry:
                    throw new WebApiDuplicateInsertException("Resource already exists");
                case MySqlErrorCode.WrongFieldSpecifier:
                    break;
                case MySqlErrorCode.ParseError:
                    break;
                case MySqlErrorCode.EmptyQuery:
                    break;
                case MySqlErrorCode.NonUniqueTable:
                    break;
                case MySqlErrorCode.InvalidDefault:
                    break;
                case MySqlErrorCode.MultiplePrimaryKey:
                    break;
                case MySqlErrorCode.TooManyKeys:
                    break;
                case MySqlErrorCode.TooManyKeysParts:
                    break;
                case MySqlErrorCode.TooLongKey:
                    break;
                case MySqlErrorCode.KeyColumnDoesNotExist:
                    break;
                case MySqlErrorCode.BlobUsedAsKey:
                    break;
                case MySqlErrorCode.TooBigFieldLength:
                    break;
                case MySqlErrorCode.WrongAutoKey:
                    break;
                case MySqlErrorCode.Ready:
                    break;
                case MySqlErrorCode.NormalShutdown:
                    break;
                case MySqlErrorCode.GotSignal:
                    break;
                case MySqlErrorCode.ShutdownComplete:
                    break;
                case MySqlErrorCode.ForcingClose:
                    break;
                case MySqlErrorCode.IPSocketError:
                    break;
                case MySqlErrorCode.NoSuchIndex:
                    break;
                case MySqlErrorCode.WrongFieldTerminators:
                    break;
                case MySqlErrorCode.BlobsAndNoTerminated:
                    break;
                case MySqlErrorCode.TextFileNotReadable:
                    break;
                case MySqlErrorCode.FileExists:
                    break;
                case MySqlErrorCode.LoadInfo:
                    break;
                case MySqlErrorCode.AlterInfo:
                    break;
                case MySqlErrorCode.WrongSubKey:
                    break;
                case MySqlErrorCode.CannotRemoveAllFields:
                    break;
                case MySqlErrorCode.CannotDropFieldOrKey:
                    break;
                case MySqlErrorCode.InsertInfo:
                    break;
                case MySqlErrorCode.UpdateTableUsed:
                    break;
                case MySqlErrorCode.NoSuchThread:
                    break;
                case MySqlErrorCode.KillDenied:
                    break;
                case MySqlErrorCode.NoTablesUsed:
                    break;
                case MySqlErrorCode.TooBigSet:
                    break;
                case MySqlErrorCode.NoUniqueLogFile:
                    break;
                case MySqlErrorCode.TableNotLockedForWrite:
                    break;
                case MySqlErrorCode.TableNotLocked:
                    break;
                case MySqlErrorCode.BlobCannotHaveDefault:
                    break;
                case MySqlErrorCode.WrongDatabaseName:
                    break;
                case MySqlErrorCode.WrongTableName:
                    break;
                case MySqlErrorCode.TooBigSelect:
                    break;
                case MySqlErrorCode.UnknownError:
                    break;
                case MySqlErrorCode.UnknownProcedure:
                    break;
                case MySqlErrorCode.WrongParameterCountToProcedure:
                    break;
                case MySqlErrorCode.WrongParametersToProcedure:
                    break;
                case MySqlErrorCode.UnknownTable:
                    break;
                case MySqlErrorCode.FieldSpecifiedTwice:
                    break;
                case MySqlErrorCode.InvalidGroupFunctionUse:
                    break;
                case MySqlErrorCode.UnsupportedExtenstion:
                    break;
                case MySqlErrorCode.TableMustHaveColumns:
                    break;
                case MySqlErrorCode.RecordFileFull:
                    break;
                case MySqlErrorCode.UnknownCharacterSet:
                    break;
                case MySqlErrorCode.TooManyTables:
                    break;
                case MySqlErrorCode.TooManyFields:
                    break;
                case MySqlErrorCode.TooBigRowSize:
                    break;
                case MySqlErrorCode.StackOverrun:
                    break;
                case MySqlErrorCode.WrongOuterJoin:
                    break;
                case MySqlErrorCode.NullColumnInIndex:
                    break;
                case MySqlErrorCode.CannotFindUDF:
                    break;
                case MySqlErrorCode.CannotInitializeUDF:
                    break;
                case MySqlErrorCode.UDFNoPaths:
                    break;
                case MySqlErrorCode.UDFExists:
                    break;
                case MySqlErrorCode.CannotOpenLibrary:
                    break;
                case MySqlErrorCode.CannotFindDLEntry:
                    break;
                case MySqlErrorCode.FunctionNotDefined:
                    break;
                case MySqlErrorCode.HostIsBlocked:
                    break;
                case MySqlErrorCode.HostNotPrivileged:
                    break;
                case MySqlErrorCode.AnonymousUser:
                    break;
                case MySqlErrorCode.PasswordNotAllowed:
                    break;
                case MySqlErrorCode.PasswordNoMatch:
                    break;
                case MySqlErrorCode.UpdateInfo:
                    break;
                case MySqlErrorCode.CannotCreateThread:
                    break;
                case MySqlErrorCode.WrongValueCountOnRow:
                    break;
                case MySqlErrorCode.CannotReopenTable:
                    break;
                case MySqlErrorCode.InvalidUseOfNull:
                    break;
                case MySqlErrorCode.RegExpError:
                    break;
                case MySqlErrorCode.MixOfGroupFunctionAndFields:
                    break;
                case MySqlErrorCode.NonExistingGrant:
                    break;
                case MySqlErrorCode.TableAccessDenied:
                    break;
                case MySqlErrorCode.ColumnAccessDenied:
                    break;
                case MySqlErrorCode.IllegalGrantForTable:
                    break;
                case MySqlErrorCode.GrantWrongHostOrUser:
                    break;
                case MySqlErrorCode.NoSuchTable:
                    break;
                case MySqlErrorCode.NonExistingTableGrant:
                    break;
                case MySqlErrorCode.NotAllowedCommand:
                    break;
                case MySqlErrorCode.SyntaxError:
                    break;
                case MySqlErrorCode.DelayedCannotChangeLock:
                    break;
                case MySqlErrorCode.TooManyDelayedThreads:
                    break;
                case MySqlErrorCode.AbortingConnection:
                    break;
                case MySqlErrorCode.PacketTooLarge:
                    break;
                case MySqlErrorCode.NetReadErrorFromPipe:
                    break;
                case MySqlErrorCode.NetFCntlError:
                    break;
                case MySqlErrorCode.NetPacketsOutOfOrder:
                    break;
                case MySqlErrorCode.NetUncompressError:
                    break;
                case MySqlErrorCode.NetReadError:
                    break;
                case MySqlErrorCode.NetReadInterrupted:
                    break;
                case MySqlErrorCode.NetErrorOnWrite:
                    break;
                case MySqlErrorCode.NetWriteInterrupted:
                    break;
                case MySqlErrorCode.TooLongString:
                    break;
                case MySqlErrorCode.TableCannotHandleBlob:
                    break;
                case MySqlErrorCode.TableCannotHandleAutoIncrement:
                    break;
                case MySqlErrorCode.DelayedInsertTableLocked:
                    break;
                case MySqlErrorCode.WrongColumnName:
                    break;
                case MySqlErrorCode.WrongKeyColumn:
                    break;
                case MySqlErrorCode.WrongMergeTable:
                    break;
                case MySqlErrorCode.DuplicateUnique:
                    break;
                case MySqlErrorCode.BlobKeyWithoutLength:
                    break;
                case MySqlErrorCode.PrimaryCannotHaveNull:
                    break;
                case MySqlErrorCode.TooManyRows:
                    break;
                case MySqlErrorCode.RequiresPrimaryKey:
                    break;
                case MySqlErrorCode.NoRAIDCompiled:
                    break;
                case MySqlErrorCode.UpdateWithoutKeysInSafeMode:
                    break;
                case MySqlErrorCode.KeyDoesNotExist:
                    break;
                case MySqlErrorCode.CheckNoSuchTable:
                    break;
                case MySqlErrorCode.CheckNotImplemented:
                    break;
                case MySqlErrorCode.CannotDoThisDuringATransaction:
                    break;
                case MySqlErrorCode.ErrorDuringCommit:
                    break;
                case MySqlErrorCode.ErrorDuringRollback:
                    break;
                case MySqlErrorCode.ErrorDuringFlushLogs:
                    break;
                case MySqlErrorCode.ErrorDuringCheckpoint:
                    break;
                case MySqlErrorCode.NewAbortingConnection:
                    break;
                case MySqlErrorCode.DumpNotImplemented:
                    break;
                case MySqlErrorCode.FlushMasterBinLogClosed:
                    break;
                case MySqlErrorCode.IndexRebuild:
                    break;
                case MySqlErrorCode.MasterError:
                    break;
                case MySqlErrorCode.MasterNetRead:
                    break;
                case MySqlErrorCode.MasterNetWrite:
                    break;
                case MySqlErrorCode.FullTextMatchingKeyNotFound:
                    break;
                case MySqlErrorCode.LockOrActiveTransaction:
                    break;
                case MySqlErrorCode.UnknownSystemVariable:
                    break;
                case MySqlErrorCode.CrashedOnUsage:
                    break;
                case MySqlErrorCode.CrashedOnRepair:
                    break;
                case MySqlErrorCode.WarningNotCompleteRollback:
                    break;
                case MySqlErrorCode.TransactionCacheFull:
                    break;
                case MySqlErrorCode.SlaveMustStop:
                    break;
                case MySqlErrorCode.SlaveNotRunning:
                    break;
                case MySqlErrorCode.BadSlave:
                    break;
                case MySqlErrorCode.MasterInfo:
                    break;
                case MySqlErrorCode.SlaveThread:
                    break;
                case MySqlErrorCode.TooManyUserConnections:
                    break;
                case MySqlErrorCode.SetConstantsOnly:
                    break;
                case MySqlErrorCode.LockWaitTimeout:
                    break;
                case MySqlErrorCode.LockTableFull:
                    break;
                case MySqlErrorCode.ReadOnlyTransaction:
                    break;
                case MySqlErrorCode.DropDatabaseWithReadLock:
                    break;
                case MySqlErrorCode.CreateDatabaseWithReadLock:
                    break;
                case MySqlErrorCode.WrongArguments:
                    break;
                case MySqlErrorCode.NoPermissionToCreateUser:
                    break;
                case MySqlErrorCode.UnionTablesInDifferentDirectory:
                    break;
                case MySqlErrorCode.LockDeadlock:
                    break;
                case MySqlErrorCode.TableCannotHandleFullText:
                    break;
                case MySqlErrorCode.CannotAddForeignConstraint:
                    break;
                case MySqlErrorCode.NoReferencedRow:
                    break;
                case MySqlErrorCode.RowIsReferenced:
                    break;
                case MySqlErrorCode.ConnectToMaster:
                    break;
                case MySqlErrorCode.QueryOnMaster:
                    break;
                case MySqlErrorCode.ErrorWhenExecutingCommand:
                    break;
                case MySqlErrorCode.WrongUsage:
                    break;
                case MySqlErrorCode.WrongNumberOfColumnsInSelect:
                    break;
                case MySqlErrorCode.CannotUpdateWithReadLock:
                    break;
                case MySqlErrorCode.MixingNotAllowed:
                    break;
                case MySqlErrorCode.DuplicateArgument:
                    break;
                case MySqlErrorCode.UserLimitReached:
                    break;
                case MySqlErrorCode.SpecifiedAccessDeniedError:
                    break;
                case MySqlErrorCode.LocalVariableError:
                    break;
                case MySqlErrorCode.GlobalVariableError:
                    break;
                case MySqlErrorCode.NotDefaultError:
                    break;
                case MySqlErrorCode.WrongValueForVariable:
                    break;
                case MySqlErrorCode.WrongTypeForVariable:
                    break;
                case MySqlErrorCode.VariableCannotBeRead:
                    break;
                case MySqlErrorCode.CannotUseOptionHere:
                    break;
                case MySqlErrorCode.NotSupportedYet:
                    break;
                case MySqlErrorCode.MasterFatalErrorReadingBinLog:
                    break;
                case MySqlErrorCode.SlaveIgnoredTable:
                    break;
                case MySqlErrorCode.IncorrectGlobalLocalVariable:
                    break;
                case MySqlErrorCode.WrongForeignKeyDefinition:
                    break;
                case MySqlErrorCode.KeyReferenceDoesNotMatchTableReference:
                    break;
                case MySqlErrorCode.OpearnColumnsError:
                    break;
                case MySqlErrorCode.SubQueryNoOneRow:
                    break;
                case MySqlErrorCode.UnknownStatementHandler:
                    break;
                case MySqlErrorCode.CorruptHelpDatabase:
                    break;
                case MySqlErrorCode.CyclicReference:
                    break;
                case MySqlErrorCode.AutoConvert:
                    break;
                case MySqlErrorCode.IllegalReference:
                    break;
                case MySqlErrorCode.DerivedMustHaveAlias:
                    break;
                case MySqlErrorCode.SelectReduced:
                    break;
                case MySqlErrorCode.TableNameNotAllowedHere:
                    break;
                case MySqlErrorCode.NotSupportedAuthMode:
                    break;
                case MySqlErrorCode.SpatialCannotHaveNull:
                    break;
                case MySqlErrorCode.CollationCharsetMismatch:
                    break;
                case MySqlErrorCode.SlaveWasRunning:
                    break;
                case MySqlErrorCode.SlaveWasNotRunning:
                    break;
                case MySqlErrorCode.TooBigForUncompress:
                    break;
                case MySqlErrorCode.ZipLibMemoryError:
                    break;
                case MySqlErrorCode.ZipLibBufferError:
                    break;
                case MySqlErrorCode.ZipLibDataError:
                    break;
                case MySqlErrorCode.CutValueGroupConcat:
                    break;
                case MySqlErrorCode.WarningTooFewRecords:
                    break;
                case MySqlErrorCode.WarningTooManyRecords:
                    break;
                case MySqlErrorCode.WarningNullToNotNull:
                    break;
                case MySqlErrorCode.WarningDataOutOfRange:
                    break;
                case MySqlErrorCode.WaningDataTruncated:
                    break;
                case MySqlErrorCode.WaningUsingOtherHandler:
                    break;
                case MySqlErrorCode.CannotAggregateTwoCollations:
                    break;
                case MySqlErrorCode.DropUserError:
                    break;
                case MySqlErrorCode.RevokeGrantsError:
                    break;
                case MySqlErrorCode.CannotAggregateThreeCollations:
                    break;
                case MySqlErrorCode.CannotAggregateNCollations:
                    break;
                case MySqlErrorCode.VariableIsNotStructure:
                    break;
                case MySqlErrorCode.UnknownCollation:
                    break;
                case MySqlErrorCode.SlaveIgnoreSSLParameters:
                    break;
                case MySqlErrorCode.ServerIsInSecureAuthMode:
                    break;
                case MySqlErrorCode.WaningFieldResolved:
                    break;
                case MySqlErrorCode.BadSlaveUntilCondition:
                    break;
                case MySqlErrorCode.MissingSkipSlave:
                    break;
                case MySqlErrorCode.ErrorUntilConditionIgnored:
                    break;
                case MySqlErrorCode.WrongNameForIndex:
                    break;
                case MySqlErrorCode.WrongNameForCatalog:
                    break;
                case MySqlErrorCode.WarningQueryCacheResize:
                    break;
                case MySqlErrorCode.BadFullTextColumn:
                    break;
                case MySqlErrorCode.UnknownKeyCache:
                    break;
                case MySqlErrorCode.WarningHostnameWillNotWork:
                    break;
                case MySqlErrorCode.UnknownStorageEngine:
                    break;
                case MySqlErrorCode.WaningDeprecatedSyntax:
                    break;
                case MySqlErrorCode.NonUpdateableTable:
                    break;
                case MySqlErrorCode.FeatureDisabled:
                    break;
                case MySqlErrorCode.OptionPreventsStatement:
                    break;
                case MySqlErrorCode.DuplicatedValueInType:
                    break;
                case MySqlErrorCode.TruncatedWrongValue:
                    break;
                case MySqlErrorCode.TooMuchAutoTimestampColumns:
                    break;
                case MySqlErrorCode.InvalidOnUpdate:
                    break;
                case MySqlErrorCode.UnsupportedPreparedStatement:
                    break;
                case MySqlErrorCode.GetErroMessage:
                    break;
                case MySqlErrorCode.GetTemporaryErrorMessage:
                    break;
                case MySqlErrorCode.UnknownTimeZone:
                    break;
                case MySqlErrorCode.WarningInvalidTimestamp:
                    break;
                case MySqlErrorCode.InvalidCharacterString:
                    break;
                case MySqlErrorCode.WarningAllowedPacketOverflowed:
                    break;
                case MySqlErrorCode.ConflictingDeclarations:
                    break;
                case MySqlErrorCode.StoredProcedureNoRecursiveCreate:
                    break;
                case MySqlErrorCode.StoredProcedureAlreadyExists:
                    break;
                case MySqlErrorCode.StoredProcedureDoesNotExist:
                    break;
                case MySqlErrorCode.StoredProcedureDropFailed:
                    break;
                case MySqlErrorCode.StoredProcedureStoreFailed:
                    break;
                case MySqlErrorCode.StoredProcedureLiLabelMismatch:
                    break;
                case MySqlErrorCode.StoredProcedureLabelRedefine:
                    break;
                case MySqlErrorCode.StoredProcedureLabelMismatch:
                    break;
                case MySqlErrorCode.StoredProcedureUninitializedVariable:
                    break;
                case MySqlErrorCode.StoredProcedureBadSelect:
                    break;
                case MySqlErrorCode.StoredProcedureBadReturn:
                    break;
                case MySqlErrorCode.StoredProcedureBadStatement:
                    break;
                case MySqlErrorCode.UpdateLogDeprecatedIgnored:
                    break;
                case MySqlErrorCode.UpdateLogDeprecatedTranslated:
                    break;
                case MySqlErrorCode.QueryInterrupted:
                    break;
                case MySqlErrorCode.StoredProcedureNumberOfArguments:
                    break;
                case MySqlErrorCode.StoredProcedureConditionMismatch:
                    break;
                case MySqlErrorCode.StoredProcedureNoReturn:
                    break;
                case MySqlErrorCode.StoredProcedureNoReturnEnd:
                    break;
                case MySqlErrorCode.StoredProcedureBadCursorQuery:
                    break;
                case MySqlErrorCode.StoredProcedureBadCursorSelect:
                    break;
                case MySqlErrorCode.StoredProcedureCursorMismatch:
                    break;
                case MySqlErrorCode.StoredProcedureAlreadyOpen:
                    break;
                case MySqlErrorCode.StoredProcedureCursorNotOpen:
                    break;
                case MySqlErrorCode.StoredProcedureUndeclaredVariabel:
                    break;
                case MySqlErrorCode.StoredProcedureWrongNumberOfFetchArguments:
                    break;
                case MySqlErrorCode.StoredProcedureFetchNoData:
                    break;
                case MySqlErrorCode.StoredProcedureDuplicateParameter:
                    break;
                case MySqlErrorCode.StoredProcedureDuplicateVariable:
                    break;
                case MySqlErrorCode.StoredProcedureDuplicateCondition:
                    break;
                case MySqlErrorCode.StoredProcedureDuplicateCursor:
                    break;
                case MySqlErrorCode.StoredProcedureCannotAlter:
                    break;
                case MySqlErrorCode.StoredProcedureSubSelectNYI:
                    break;
                case MySqlErrorCode.StatementNotAllowedInStoredFunctionOrTrigger:
                    break;
                case MySqlErrorCode.StoredProcedureVariableConditionAfterCursorHandler:
                    break;
                case MySqlErrorCode.StoredProcedureCursorAfterHandler:
                    break;
                case MySqlErrorCode.StoredProcedureCaseNotFound:
                    break;
                case MySqlErrorCode.FileParserTooBigFile:
                    break;
                case MySqlErrorCode.FileParserBadHeader:
                    break;
                case MySqlErrorCode.FileParserEOFInComment:
                    break;
                case MySqlErrorCode.FileParserErrorInParameter:
                    break;
                case MySqlErrorCode.FileParserEOFInUnknownParameter:
                    break;
                case MySqlErrorCode.ViewNoExplain:
                    break;
                case MySqlErrorCode.FrmUnknownType:
                    break;
                case MySqlErrorCode.WrongObject:
                    break;
                case MySqlErrorCode.NonUpdateableColumn:
                    break;
                case MySqlErrorCode.ViewSelectDerived:
                    break;
                case MySqlErrorCode.ViewSelectClause:
                    break;
                case MySqlErrorCode.ViewSelectVariable:
                    break;
                case MySqlErrorCode.ViewSelectTempTable:
                    break;
                case MySqlErrorCode.ViewWrongList:
                    break;
                case MySqlErrorCode.WarningViewMerge:
                    break;
                case MySqlErrorCode.WarningViewWithoutKey:
                    break;
                case MySqlErrorCode.ViewInvalid:
                    break;
                case MySqlErrorCode.StoredProcedureNoDropStoredProcedure:
                    break;
                case MySqlErrorCode.StoredProcedureGotoInHandler:
                    break;
                case MySqlErrorCode.TriggerAlreadyExists:
                    break;
                case MySqlErrorCode.TriggerDoesNotExist:
                    break;
                case MySqlErrorCode.TriggerOnViewOrTempTable:
                    break;
                case MySqlErrorCode.TriggerCannotChangeRow:
                    break;
                case MySqlErrorCode.TriggerNoSuchRowInTrigger:
                    break;
                case MySqlErrorCode.NoDefaultForField:
                    break;
                case MySqlErrorCode.DivisionByZero:
                    break;
                case MySqlErrorCode.TruncatedWrongValueForField:
                    break;
                case MySqlErrorCode.IllegalValueForType:
                    break;
                case MySqlErrorCode.ViewNonUpdatableCheck:
                    break;
                case MySqlErrorCode.ViewCheckFailed:
                    break;
                case MySqlErrorCode.PrecedureAccessDenied:
                    break;
                case MySqlErrorCode.RelayLogFail:
                    break;
                case MySqlErrorCode.PasswordLength:
                    break;
                case MySqlErrorCode.UnknownTargetBinLog:
                    break;
                case MySqlErrorCode.IOErrorLogIndexRead:
                    break;
                case MySqlErrorCode.BinLogPurgeProhibited:
                    break;
                case MySqlErrorCode.FSeekFail:
                    break;
                case MySqlErrorCode.BinLogPurgeFatalError:
                    break;
                case MySqlErrorCode.LogInUse:
                    break;
                case MySqlErrorCode.LogPurgeUnknownError:
                    break;
                case MySqlErrorCode.RelayLogInit:
                    break;
                case MySqlErrorCode.NoBinaryLogging:
                    break;
                case MySqlErrorCode.ReservedSyntax:
                    break;
                case MySqlErrorCode.WSAStartupFailed:
                    break;
                case MySqlErrorCode.DifferentGroupsProcedure:
                    break;
                case MySqlErrorCode.NoGroupForProcedure:
                    break;
                case MySqlErrorCode.OrderWithProcedure:
                    break;
                case MySqlErrorCode.LoggingProhibitsChangingOf:
                    break;
                case MySqlErrorCode.NoFileMapping:
                    break;
                case MySqlErrorCode.WrongMagic:
                    break;
                case MySqlErrorCode.PreparedStatementManyParameters:
                    break;
                case MySqlErrorCode.KeyPartZero:
                    break;
                case MySqlErrorCode.ViewChecksum:
                    break;
                case MySqlErrorCode.ViewMultiUpdate:
                    break;
                case MySqlErrorCode.ViewNoInsertFieldList:
                    break;
                case MySqlErrorCode.ViewDeleteMergeView:
                    break;
                case MySqlErrorCode.CannotUser:
                    break;
                case MySqlErrorCode.XAERNotA:
                    break;
                case MySqlErrorCode.XAERInvalid:
                    break;
                case MySqlErrorCode.XAERRemoveFail:
                    break;
                case MySqlErrorCode.XAEROutside:
                    break;
                case MySqlErrorCode.XAERRemoveError:
                    break;
                case MySqlErrorCode.XARBRollback:
                    break;
                case MySqlErrorCode.NonExistingProcedureGrant:
                    break;
                case MySqlErrorCode.ProcedureAutoGrantFail:
                    break;
                case MySqlErrorCode.ProcedureAutoRevokeFail:
                    break;
                case MySqlErrorCode.DataTooLong:
                    break;
                case MySqlErrorCode.StoredProcedureSQLState:
                    break;
                case MySqlErrorCode.StartupError:
                    break;
                case MySqlErrorCode.LoadFromFixedSizeRowsToVariable:
                    break;
                case MySqlErrorCode.CannotCreateUserWithGrant:
                    break;
                case MySqlErrorCode.WrongValueForType:
                    break;
                case MySqlErrorCode.TableDefinitionChanged:
                    break;
                case MySqlErrorCode.StoredProcedureDuplicateHandler:
                    break;
                case MySqlErrorCode.StoredProcedureNotVariableArgument:
                    break;
                case MySqlErrorCode.StoredProcedureNoReturnSet:
                    break;
                case MySqlErrorCode.CannotCreateGeometryObject:
                    break;
                case MySqlErrorCode.FailedRoutineBreaksBinLog:
                    break;
                case MySqlErrorCode.BinLogUnsafeRoutine:
                    break;
                case MySqlErrorCode.BinLogCreateRoutineNeedSuper:
                    break;
                case MySqlErrorCode.ExecuteStatementWithOpenCursor:
                    break;
                case MySqlErrorCode.StatementHasNoOpenCursor:
                    break;
                case MySqlErrorCode.CommitNotAllowedIfStoredFunctionOrTrigger:
                    break;
                case MySqlErrorCode.NoDefaultForViewField:
                    break;
                case MySqlErrorCode.StoredProcedureNoRecursion:
                    break;
                case MySqlErrorCode.TooBigScale:
                    break;
                case MySqlErrorCode.TooBigPrecision:
                    break;
                case MySqlErrorCode.MBiggerThanD:
                    break;
                case MySqlErrorCode.WrongLockOfSystemTable:
                    break;
                case MySqlErrorCode.ConnectToForeignDataSource:
                    break;
                case MySqlErrorCode.QueryOnForeignDataSource:
                    break;
                case MySqlErrorCode.ForeignDataSourceDoesNotExist:
                    break;
                case MySqlErrorCode.ForeignDataStringInvalidCannotCreate:
                    break;
                case MySqlErrorCode.ForeignDataStringInvalid:
                    break;
                case MySqlErrorCode.CannotCreateFederatedTable:
                    break;
                case MySqlErrorCode.TriggerInWrongSchema:
                    break;
                case MySqlErrorCode.StackOverrunNeedMore:
                    break;
                case MySqlErrorCode.TooLongBody:
                    break;
                case MySqlErrorCode.WarningCannotDropDefaultKeyCache:
                    break;
                case MySqlErrorCode.TooBigDisplayWidth:
                    break;
                case MySqlErrorCode.XAERDuplicateID:
                    break;
                case MySqlErrorCode.DateTimeFunctionOverflow:
                    break;
                case MySqlErrorCode.CannotUpdateUsedTableInStoredFunctionOrTrigger:
                    break;
                case MySqlErrorCode.ViewPreventUpdate:
                    break;
                case MySqlErrorCode.PreparedStatementNoRecursion:
                    break;
                case MySqlErrorCode.StoredProcedureCannotSetAutoCommit:
                    break;
                case MySqlErrorCode.MalformedDefiner:
                    break;
                case MySqlErrorCode.ViewFrmNoUser:
                    break;
                case MySqlErrorCode.ViewOtherUser:
                    break;
                case MySqlErrorCode.NoSuchUser:
                    break;
                case MySqlErrorCode.ForbidSchemaChange:
                    break;
                case MySqlErrorCode.RowIsReferenced2:
                    break;
                case MySqlErrorCode.NoReferencedRow2:
                    break;
                case MySqlErrorCode.StoredProcedureBadVariableShadow:
                    break;
                case MySqlErrorCode.TriggerNoDefiner:
                    break;
                case MySqlErrorCode.OldFileFormat:
                    break;
                case MySqlErrorCode.StoredProcedureRecursionLimit:
                    break;
                case MySqlErrorCode.StoredProcedureTableCorrupt:
                    break;
                case MySqlErrorCode.StoredProcedureWrongName:
                    break;
                case MySqlErrorCode.TableNeedsUpgrade:
                    break;
                case MySqlErrorCode.StoredProcedureNoAggregate:
                    break;
                case MySqlErrorCode.MaxPreparedStatementCountReached:
                    break;
                case MySqlErrorCode.ViewRecursive:
                    break;
                case MySqlErrorCode.NonGroupingFieldUsed:
                    break;
                case MySqlErrorCode.TableCannotHandleSpatialKeys:
                    break;
                case MySqlErrorCode.NoTriggersOnSystemSchema:
                    break;
                case MySqlErrorCode.RemovedSpaces:
                    break;
                case MySqlErrorCode.AutoIncrementReadFailed:
                    break;
                case MySqlErrorCode.UserNameError:
                    break;
                case MySqlErrorCode.HostNameError:
                    break;
                case MySqlErrorCode.WrongStringLength:
                    break;
                case MySqlErrorCode.NonInsertableTable:
                    break;
                case MySqlErrorCode.AdminWrongMergeTable:
                    break;
                case MySqlErrorCode.TooHighLevelOfNestingForSelect:
                    break;
                case MySqlErrorCode.NameBecomesEmpty:
                    break;
                case MySqlErrorCode.AmbiguousFieldTerm:
                    break;
                case MySqlErrorCode.ForeignServerExists:
                    break;
                case MySqlErrorCode.ForeignServerDoesNotExist:
                    break;
                case MySqlErrorCode.IllegalHACreateOption:
                    break;
                case MySqlErrorCode.PartitionRequiresValues:
                    break;
                case MySqlErrorCode.PartitionWrongValues:
                    break;
                case MySqlErrorCode.PartitionMaxValue:
                    break;
                case MySqlErrorCode.PartitionSubPartition:
                    break;
                case MySqlErrorCode.PartitionSubPartMix:
                    break;
                case MySqlErrorCode.PartitionWrongNoPart:
                    break;
                case MySqlErrorCode.PartitionWrongNoSubPart:
                    break;
                case MySqlErrorCode.WrongExpressionInParitionFunction:
                    break;
                case MySqlErrorCode.NoConstantExpressionInRangeOrListError:
                    break;
                case MySqlErrorCode.FieldNotFoundPartitionErrror:
                    break;
                case MySqlErrorCode.ListOfFieldsOnlyInHash:
                    break;
                case MySqlErrorCode.InconsistentPartitionInfo:
                    break;
                case MySqlErrorCode.PartitionFunctionNotAllowed:
                    break;
                case MySqlErrorCode.PartitionsMustBeDefined:
                    break;
                case MySqlErrorCode.RangeNotIncreasing:
                    break;
                case MySqlErrorCode.InconsistentTypeOfFunctions:
                    break;
                case MySqlErrorCode.MultipleDefinitionsConstantInListPartition:
                    break;
                case MySqlErrorCode.PartitionEntryError:
                    break;
                case MySqlErrorCode.MixHandlerError:
                    break;
                case MySqlErrorCode.PartitionNotDefined:
                    break;
                case MySqlErrorCode.TooManyPartitions:
                    break;
                case MySqlErrorCode.SubPartitionError:
                    break;
                case MySqlErrorCode.CannotCreateHandlerFile:
                    break;
                case MySqlErrorCode.BlobFieldInPartitionFunction:
                    break;
                case MySqlErrorCode.UniqueKeyNeedAllFieldsInPartitioningFunction:
                    break;
                case MySqlErrorCode.NoPartitions:
                    break;
                case MySqlErrorCode.PartitionManagementOnNoPartitioned:
                    break;
                case MySqlErrorCode.ForeignKeyOnPartitioned:
                    break;
                case MySqlErrorCode.DropPartitionNonExistent:
                    break;
                case MySqlErrorCode.DropLastPartition:
                    break;
                case MySqlErrorCode.CoalesceOnlyOnHashPartition:
                    break;
                case MySqlErrorCode.ReorganizeHashOnlyOnSameNumber:
                    break;
                case MySqlErrorCode.ReorganizeNoParameter:
                    break;
                case MySqlErrorCode.OnlyOnRangeListPartition:
                    break;
                case MySqlErrorCode.AddPartitionSubPartition:
                    break;
                case MySqlErrorCode.AddPartitionNoNewPartition:
                    break;
                case MySqlErrorCode.CoalescePartitionNoPartition:
                    break;
                case MySqlErrorCode.ReorganizePartitionNotExist:
                    break;
                case MySqlErrorCode.SameNamePartition:
                    break;
                case MySqlErrorCode.NoBinLog:
                    break;
                case MySqlErrorCode.ConsecutiveReorganizePartitions:
                    break;
                case MySqlErrorCode.ReorganizeOutsideRange:
                    break;
                case MySqlErrorCode.PartitionFunctionFailure:
                    break;
                case MySqlErrorCode.PartitionStateError:
                    break;
                case MySqlErrorCode.LimitedPartitionRange:
                    break;
                case MySqlErrorCode.PluginIsNotLoaded:
                    break;
                case MySqlErrorCode.WrongValue:
                    break;
                case MySqlErrorCode.NoPartitionForGivenValue:
                    break;
                case MySqlErrorCode.FileGroupOptionOnlyOnce:
                    break;
                case MySqlErrorCode.CreateFileGroupFailed:
                    break;
                case MySqlErrorCode.DropFileGroupFailed:
                    break;
                case MySqlErrorCode.TableSpaceAutoExtend:
                    break;
                case MySqlErrorCode.WrongSizeNumber:
                    break;
                case MySqlErrorCode.SizeOverflow:
                    break;
                case MySqlErrorCode.AlterFileGroupFailed:
                    break;
                case MySqlErrorCode.BinLogRowLogginFailed:
                    break;
                case MySqlErrorCode.BinLogRowWrongTableDefinition:
                    break;
                case MySqlErrorCode.BinLogRowRBRToSBR:
                    break;
                case MySqlErrorCode.EventAlreadyExists:
                    break;
                case MySqlErrorCode.EventStoreFailed:
                    break;
                case MySqlErrorCode.EventDoesNotExist:
                    break;
                case MySqlErrorCode.EventCannotAlter:
                    break;
                case MySqlErrorCode.EventDropFailed:
                    break;
                case MySqlErrorCode.EventIntervalNotPositiveOrTooBig:
                    break;
                case MySqlErrorCode.EventEndsBeforeStarts:
                    break;
                case MySqlErrorCode.EventExecTimeInThePast:
                    break;
                case MySqlErrorCode.EventOpenTableFailed:
                    break;
                case MySqlErrorCode.EventNeitherMExpresssionNorMAt:
                    break;
                case MySqlErrorCode.ColumnCountDoesNotMatchCorrupted:
                    break;
                case MySqlErrorCode.CannotLoadFromTable:
                    break;
                case MySqlErrorCode.EventCannotDelete:
                    break;
                case MySqlErrorCode.EventCompileError:
                    break;
                case MySqlErrorCode.EventSameName:
                    break;
                case MySqlErrorCode.EventDataTooLong:
                    break;
                case MySqlErrorCode.DropIndexForeignKey:
                    break;
                case MySqlErrorCode.WarningDeprecatedSyntaxWithVersion:
                    break;
                case MySqlErrorCode.CannotWriteLockLogTable:
                    break;
                case MySqlErrorCode.CannotLockLogTable:
                    break;
                case MySqlErrorCode.ForeignDuplicateKey:
                    break;
                case MySqlErrorCode.ColumnCountDoesNotMatchPleaseUpdate:
                    break;
                case MySqlErrorCode.TemoraryTablePreventSwitchOutOfRBR:
                    break;
                case MySqlErrorCode.StoredFunctionPreventsSwitchBinLogFormat:
                    break;
                case MySqlErrorCode.NDBCannotSwitchBinLogFormat:
                    break;
                case MySqlErrorCode.PartitionNoTemporary:
                    break;
                case MySqlErrorCode.PartitionConstantDomain:
                    break;
                case MySqlErrorCode.PartitionFunctionIsNotAllowed:
                    break;
                case MySqlErrorCode.DDLLogError:
                    break;
                case MySqlErrorCode.NullInValuesLessThan:
                    break;
                case MySqlErrorCode.WrongPartitionName:
                    break;
                case MySqlErrorCode.CannotChangeTransactionIsolation:
                    break;
                case MySqlErrorCode.DuplicateEntryAutoIncrementCase:
                    break;
                case MySqlErrorCode.EventModifyQueueError:
                    break;
                case MySqlErrorCode.EventSetVariableError:
                    break;
                case MySqlErrorCode.PartitionMergeError:
                    break;
                case MySqlErrorCode.CannotActivateLog:
                    break;
                case MySqlErrorCode.RBRNotAvailable:
                    break;
                case MySqlErrorCode.Base64DecodeError:
                    break;
                case MySqlErrorCode.EventRecursionForbidden:
                    break;
                case MySqlErrorCode.EventsDatabaseError:
                    break;
                case MySqlErrorCode.OnlyIntegersAllowed:
                    break;
                case MySqlErrorCode.UnsupportedLogEngine:
                    break;
                case MySqlErrorCode.BadLogStatement:
                    break;
                case MySqlErrorCode.CannotRenameLogTable:
                    break;
                case MySqlErrorCode.WrongParameterCountToNativeFCT:
                    break;
                case MySqlErrorCode.WrongParametersToNativeFCT:
                    break;
                case MySqlErrorCode.WrongParametersToStoredFCT:
                    break;
                case MySqlErrorCode.NativeFCTNameCollision:
                    break;
                case MySqlErrorCode.DuplicateEntryWithKeyName:
                    break;
                case MySqlErrorCode.BinLogPurgeEMFile:
                    break;
                case MySqlErrorCode.EventCannotCreateInThePast:
                    break;
                case MySqlErrorCode.EventCannotAlterInThePast:
                    break;
                case MySqlErrorCode.SlaveIncident:
                    break;
                case MySqlErrorCode.NoPartitionForGivenValueSilent:
                    break;
                case MySqlErrorCode.BinLogUnsafeStatement:
                    break;
                case MySqlErrorCode.SlaveFatalError:
                    break;
                case MySqlErrorCode.SlaveRelayLogReadFailure:
                    break;
                case MySqlErrorCode.SlaveRelayLogWriteFailure:
                    break;
                case MySqlErrorCode.SlaveCreateEventFailure:
                    break;
                case MySqlErrorCode.SlaveMasterComFailure:
                    break;
                case MySqlErrorCode.BinLogLoggingImpossible:
                    break;
                case MySqlErrorCode.ViewNoCreationContext:
                    break;
                case MySqlErrorCode.ViewInvalidCreationContext:
                    break;
                case MySqlErrorCode.StoredRoutineInvalidCreateionContext:
                    break;
                case MySqlErrorCode.TiggerCorruptedFile:
                    break;
                case MySqlErrorCode.TriggerNoCreationContext:
                    break;
                case MySqlErrorCode.TriggerInvalidCreationContext:
                    break;
                case MySqlErrorCode.EventInvalidCreationContext:
                    break;
                case MySqlErrorCode.TriggerCannotOpenTable:
                    break;
                case MySqlErrorCode.CannoCreateSubRoutine:
                    break;
                case MySqlErrorCode.SlaveAmbiguousExecMode:
                    break;
                case MySqlErrorCode.NoFormatDescriptionEventBeforeBinLogStatement:
                    break;
                case MySqlErrorCode.SlaveCorruptEvent:
                    break;
                case MySqlErrorCode.LoadDataInvalidColumn:
                    break;
                case MySqlErrorCode.LogPurgeNoFile:
                    break;
                case MySqlErrorCode.XARBTimeout:
                    break;
                case MySqlErrorCode.XARBDeadlock:
                    break;
                case MySqlErrorCode.NeedRePrepare:
                    break;
                case MySqlErrorCode.DelayedNotSupported:
                    break;
                case MySqlErrorCode.WarningNoMasterInfo:
                    break;
                case MySqlErrorCode.WarningOptionIgnored:
                    break;
                case MySqlErrorCode.WarningPluginDeleteBuiltIn:
                    break;
                case MySqlErrorCode.WarningPluginBusy:
                    break;
                case MySqlErrorCode.VariableIsReadonly:
                    break;
                case MySqlErrorCode.WarningEngineTransactionRollback:
                    break;
                case MySqlErrorCode.SlaveHeartbeatFailure:
                    break;
                case MySqlErrorCode.SlaveHeartbeatValueOutOfRange:
                    break;
                case MySqlErrorCode.NDBReplicationSchemaError:
                    break;
                case MySqlErrorCode.ConflictFunctionParseError:
                    break;
                case MySqlErrorCode.ExcepionsWriteError:
                    break;
                case MySqlErrorCode.TooLongTableComment:
                    break;
                case MySqlErrorCode.TooLongFieldComment:
                    break;
                case MySqlErrorCode.FunctionInExistentNameCollision:
                    break;
                case MySqlErrorCode.DatabaseNameError:
                    break;
                case MySqlErrorCode.TableNameErrror:
                    break;
                case MySqlErrorCode.PartitionNameError:
                    break;
                case MySqlErrorCode.SubPartitionNameError:
                    break;
                case MySqlErrorCode.TemporaryNameError:
                    break;
                case MySqlErrorCode.RenamedNameError:
                    break;
                case MySqlErrorCode.TooManyConcurrentTransactions:
                    break;
                case MySqlErrorCode.WarningNonASCIISeparatorNotImplemented:
                    break;
                case MySqlErrorCode.DebugSyncTimeout:
                    break;
                case MySqlErrorCode.DebugSyncHitLimit:
                    break;
                default:
                    break;
            }
        }
    }
}
