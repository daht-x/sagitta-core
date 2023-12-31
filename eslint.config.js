import { FlatCompat } from "@eslint/eslintrc";
import eslintJs from "@eslint/js";
import importPlugin from "eslint-plugin-import";
import { ruleCompliance, ruleSeverity } from "./linter-configuration.js";

const eslintJsConfiguration = eslintJs.configs.recommended;
const compatibilityManager = new FlatCompat();
const airbnbConfiguration = compatibilityManager.extends("airbnb-base");
const rootConfiguration = [
	eslintJsConfiguration,
	...airbnbConfiguration,
	{
		plugins: {
			importPlugin
		},
		rules: {
			indent: [
				ruleSeverity.error,
				"tab"
			],
			"no-tabs": ruleSeverity.disabled,
			"padded-blocks": [
				ruleSeverity.error,
				ruleCompliance.never
			],
			"brace-style": [
				ruleSeverity.error,
				"allman"
			],
			quotes: [
				ruleSeverity.error,
				"double"
			],
			"arrow-parens": [
				ruleSeverity.error,
				"as-needed"
			],
			"comma-dangle": [
				ruleSeverity.error,
				ruleCompliance.never
			],
			"import/extensions": [
				ruleSeverity.error,
				ruleCompliance.always
			],
			"import/no-extraneous-dependencies": ruleSeverity.disabled,
			"import/exports-last": ruleSeverity.error
		}
	}
];
export default rootConfiguration;
